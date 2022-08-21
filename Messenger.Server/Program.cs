using Messenger.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddSingleton<IUserRepository>(new UserRepository());

builder.Services.AddSwaggerGen(options => Swagger.GenerateConfig(options));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => AddJwtBearer
    .GenerateConfig(options, builder));

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

app.MapGet("/users", [Authorize] async (DataBaseContext dataBase) => await dataBase.Users.ToListAsync());

app.MapGet("/users/{guid}", async (DataBaseContext dataBase, Guid guid) =>
    await dataBase.Users.FirstOrDefaultAsync(c => c.Guid == guid) is User userModel
    ? Results.Ok(userModel)
    : Results.NotFound());

app.MapPost("/users/", async (DataBaseContext dataBase, [FromBody] User userModel) =>
{
    userModel.Guid = Guid.NewGuid();
    await dataBase.Users.AddAsync(userModel);
    await dataBase.SaveChangesAsync();
    return Results.Created($"/users/{userModel.Guid}", userModel);
});

app.MapDelete("/users/{guid}", async (DataBaseContext dataBase, Guid guid) =>
{
    var user = await dataBase.Users.FirstOrDefaultAsync(c => c.Guid == guid);

    if (user == null) return Results.NotFound();
    
    dataBase.Users.Remove(user);

    await dataBase.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/login", [AllowAnonymous] (User user,
    ITokenService tokenService,
    IUserRepository userRepository) =>
{
    User userModel = new User
    {
        UserName = user.UserName,
        Password = user.Password
    };

    var userDto = userRepository.GetUser(userModel);

    if (userDto == null) return Task.FromResult(Results.Unauthorized());

    var token = tokenService.BuildToken(
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"],
        userDto
        );

    return Task.FromResult(Results.Ok(token));

});


app.MapGet("/", () => "Hello Messenger!");

app.Run();