using AutoMapper;
using Messenger.Domains.Dtos;
using Messenger.Domains.Models;
using Messenger.Server.Data;
using Messenger.Server.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(options => Swagger.GenerateConfig(options));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();


builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => AddJwtBearer
    .GenerateConfig(options, builder));

builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dataBase = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    dataBase.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/v1/users", async (IUserRepository repository, IMapper mapper) =>
{
    var users = await repository.GetAllUsersAsync();

    return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
});



app.MapGet("/api/v1/users/{guid}", async (IUserRepository repository, IMapper mapper, Guid guid) =>
await repository.GetUserByGuidAsync(guid) is User userModel
    ? Results.Ok(mapper.Map<UserReadDto>(userModel))
: Results.NotFound());

app.MapPost("/api/v1/auth", async (IUserRepository repository, ITokenService tokenService, IMapper mapper, UserAuthDto user) =>
{

    var userDto = await repository.AuthUserAsync(user);

    if (userDto == null) return Task.FromResult(Results.Unauthorized());

    var token = tokenService.BuildToken(
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"],
        userDto
        );

    return Task.FromResult(Results.Ok(token));

});



app.MapGet("/users/{guid}", async (IUserRepository repository, IMapper mapper, Guid guid) =>
{

    var searchUser = repository.GetUserByGuidAsync(guid);

    if (searchUser == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(searchUser));

    /*await dataBase.Users.FirstOrDefaultAsync(c => c.GlobalGuid == guid) is User userModel
    ? Results.Ok(userModel)
    : Results.NotFound()*/

});
app.MapPost("/users/", async (DataBaseContext dataBase, [FromBody] User userModel) =>
{
    userModel.GlobalGuid = Guid.NewGuid();
    await dataBase.Users.AddAsync(userModel);
    await dataBase.SaveChangesAsync();
    return Results.Created($"/users/{userModel.GlobalGuid}", userModel);
});

app.MapDelete("/users/{guid}", async (DataBaseContext dataBase, Guid guid) =>
{
    var user = await dataBase.Users.FirstOrDefaultAsync(c => c.GlobalGuid == guid);

    if (user == null) return Results.NotFound();

    dataBase.Users.Remove(user);

    await dataBase.SaveChangesAsync();
    return Results.NoContent();
});

//app.MapPost("/login", [AllowAnonymous]
//async (User user, ITokenService tokenService, IUserRepository userRepository) =>
//{
//    User userModel = new User
//    {
//        GlobalGuid = user.GlobalGuid,
//        UserName = user.UserName,
//        Password = user.Password
//    };

//    var userDto = await userRepository.AuthUserAsync(userModel);

//    if (userDto == null) return Task.FromResult(Results.Unauthorized());

//    var token = tokenService.BuildToken(
//        builder.Configuration["Jwt:Key"],
//        builder.Configuration["Jwt:Issuer"],
//        userDto
//        );

//    return Task.FromResult(Results.Ok(token));

//});

app.MapGet("/", () => "Hello Messenger!");

app.Run();