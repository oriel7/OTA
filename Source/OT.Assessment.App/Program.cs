using Dapper;
using MassTransit;
using OT.Assessment.Core.Domain.Constants;
using OT.Assessment.Core.Domain.DTO;
using OT.Assessment.Core.Repository.Repositories;
using OT.Assessment.Core.Services.Services;
using OT.Assessment.Infrastructure.Persistence.Contexts;
using OT.Assessment.Infrastructure.Persistence.Handler;
using OT.Assessment.Infrastructure.Repository.Repositories;
using OT.Assessment.Infrastructure.Service.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

SetupSwagger(builder);

//SqlMapper.AddTypeHandler(new DapperGuidTypeHandler());
//SqlMapper.RemoveTypeMap(typeof(Guid));
//SqlMapper.RemoveTypeMap(typeof(Guid?));

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
    builder.Services.AddSingleton<DapperContext>();
    builder.Services.AddScoped<IPlayerCasinoWagerRepository, PlayerCasinoWagerRepository>();
    builder.Services.AddScoped<IPlayerCasinoWagerService, PlayerCasinoWagerService>();
}

static void SetMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.UsingRabbitMq((context, configurator) =>
        {
            configurator.Host(new Uri(RabbitMqConstants.RootUri), h =>
            {
                h.Username(RabbitMqConstants.UserName);
                h.Password(RabbitMqConstants.Password);
            });

            configurator.ConfigureEndpoints(context);
        });
    });
}