using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddSingleton<IUserRepository>(new UserRepository());
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", [AllowAnonymous] (UserModel user,
    ITokenService tokenService,
    IUserRepository userRepository) =>
{
    UserModel userModel = new UserModel
    {
        UserName = user.UserName,
        Password = user.Password,
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


app.MapGet("/", [Authorize] () => "Hello Messenger!");

app.Run();