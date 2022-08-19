using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class Swagger
{

    public static void GenerateConfig(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "MAUI Messenger web api",
            Description = "This page presents methods for implementing the basic functionality of the web api",
            TermsOfService = new Uri("https://github.com/maui-messanger/maui-messenger"),
            Contact = new OpenApiContact
            {
                Name = "Contacts",
                Url = new Uri("https://github.com/maui-messanger/maui-messenger")
            },
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://github.com/maui-messanger/maui-messenger")
            }
        });
    }
}