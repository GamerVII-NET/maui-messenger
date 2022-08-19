var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddSingleton<IUserRepository>(new UserRepository());

builder.Services.AddSwaggerGen(options => Swagger.GenerateConfig(options));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => AddJwtBearer
    .GenerateConfig(options, builder));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index => new UserDto($"Test {index}", "aewfnawofn"));    

    return forecast;
})
.WithName("GetWeatherForecast");

//app.MapPost("/login", [AllowAnonymous] (UserModel user,
//    ITokenService tokenService,
//    IUserRepository userRepository) =>
//{
//    UserModel userModel = new UserModel
//    {
//        UserName = user.UserName,
//        Password = user.Password
//    };

//    var userDto = userRepository.GetUser(userModel);

//    if (userDto == null) return Task.FromResult(Results.Unauthorized());

//    var token = tokenService.BuildToken(
//        builder.Configuration["Jwt:Key"],
//        builder.Configuration["Jwt:Issuer"],
//        userDto
//        );

//    return Task.FromResult(Results.Ok(token));

//});


//app.MapGet("/", () => "Hello Messenger!");

app.Run();