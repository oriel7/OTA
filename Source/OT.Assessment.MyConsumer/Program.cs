using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Services.Services;
using OT.Assessment.Infrastructure.Service.Services;
using OT.Assessment.MyConsumer.Consumers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

SetServiceDependencies(builder);

SetMassTransit(builder);

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.Run();

static void SetServiceDependencies(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ICasinoWagerService, CasinoWagerService>();
    builder.Services.AddScoped<IPlayerService, PlayerService>();
}

static void SetMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.AddConsumer<CreateCasinoWagerConsumer>();
        busConfigurator.AddConsumer<GetCasinoWagerConsumer>();
        busConfigurator.AddConsumer<GetTopSpenderConsumer>();

        busConfigurator.UsingRabbitMq((context, configurator) =>
        {
            //configurator.Host(new Uri(builder.Configuration["MessageBroker.Host"]!), h =>
            //{
            //    h.Username(builder.Configuration["MessageBroker.Username"]);
            //    h.Password(builder.Configuration["MessageBroker.Password"]);
            //});

            configurator.Host(new Uri(RabbitMqConstants.RabbitMqRootUri), h =>
            {
                h.Username(RabbitMqConstants.UserName);
                h.Password(RabbitMqConstants.Password);
            });

            configurator.ReceiveEndpoint("create-casinowager", e =>
            {
                e.Consumer<CreateCasinoWagerConsumer>(context);
            });

            configurator.ReceiveEndpoint("get-casinowager", e =>
            {
                e.Consumer<GetCasinoWagerConsumer>(context);
            });

            configurator.ReceiveEndpoint("get-top-spender", e =>
            {
                e.Consumer<GetTopSpenderConsumer>(context);
            });
        });
    });
}

//builder.Services.AddMassTransit(x =>
//{
//    //x.AddConsumer<>
//})

//var host = Host.CreateDefaultBuilder(args)
//    .ConfigureAppConfiguration(config =>
//    {
//        config.SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .Build();
//    })
//    .ConfigureServices((context, services) =>
//    {
//        //configure services

//    })
//    .UseMassTransit(busConfiguration =>
//    {
//        busConfiguration.
//    }
//    )
//    .UseSerilog((context, configuration) =>
//        configuration.ReadFrom.Configuration(context.Configuration))
//    .Build();

//var logger = builder.Host.S host.Services.GetRequiredService<ILogger<Program>>();
//logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

//await host.RunAsync();

//logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);