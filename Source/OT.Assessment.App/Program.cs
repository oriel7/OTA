using MassTransit;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Services.Services;
using OT.Assessment.Infrastructure.Service.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

SetupSwagger(builder);

SetServiceDependencies(builder);

SetMassTransit(builder);

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
        opts.DocumentTitle = "OT Assessment App";
        opts.DisplayRequestDuration();
    });
}

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetupSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

static void SetServiceDependencies(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ICasinoWagerService, CasinoWagerService>();
    builder.Services.AddScoped<IPlayerService, PlayerService>();
}

static void SetMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
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

            configurator.ConfigureEndpoints(context);
        });
    });
}