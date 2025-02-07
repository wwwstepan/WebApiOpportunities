using MassTransit;
using NetCoreApp.Bus;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApiOpportunities.GRpcServices;
using WebApiOpportunities.Middlewares;
using StackExchange.Redis;
using NetCoreApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddControllers();

// Configure Swagger
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

// Configure RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ChangeUserConsumer>();
    x.AddConsumer<ChangeOrderConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("user-queue", e => e.Consumer<ChangeUserConsumer>());
        cfg.ReceiveEndpoint("order-queue", e => e.Consumer<ChangeOrderConsumer>());
    });
});


// Add Redis to the DI container
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddSingleton<RedisService>();

var app = builder.Build();

app.MapGrpcService<CalculateProcessingService>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();

app.Run();
