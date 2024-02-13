using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApiOpportunities.Middlewares;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 8 using Swagger example",
        Description = "",
        TermsOfService = new Uri("https://learn.microsoft.com/"),
        Contact = new OpenApiContact
        {
            Name = "Wisit our site",
            Url = new Uri("https://learn.microsoft.com/")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://licenses.nuget.org/MIT")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();

app.Run();
