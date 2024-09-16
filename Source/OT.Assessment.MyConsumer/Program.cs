using MassTransit;
using Microsoft.AspNetCore.Builder;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Repository.Repositories;
using OT.Assessment.Core.Services.Services;
using OT.Assessment.Infrastructure.Persistence.Contexts;
using OT.Assessment.Infrastructure.Repository.Repositories;
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
    builder.Services.AddSingleton<DapperContext>();
    builder.Services.AddScoped<IPlayerCasinoWagerRepository, PlayerCasinoWagerRepository>();
    builder.Services.AddScoped<IPlayerCasinoWagerService, PlayerCasinoWagerService>();
}

static void SetMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.AddConsumer<CreateCasinoWagerConsumer>();

        busConfigurator.UsingRabbitMq((context, configurator) =>
        {
            configurator.Host(new Uri(RabbitMqConstants.RootUri), h =>
            {
                h.Username(RabbitMqConstants.UserName);
                h.Password(RabbitMqConstants.Password);
            });

            configurator.ReceiveEndpoint(RabbitMqConstants.CreateCasinoWagerQueueName, e =>
            {
                e.Consumer<CreateCasinoWagerConsumer>(context);
            });
        });
    });
}
