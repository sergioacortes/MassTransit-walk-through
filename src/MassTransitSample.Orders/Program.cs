using MassTransit;
using MassTransitSample.Extensions;
using MassTransitSample.Orders.BackgroundServices;
using MassTransitSample.Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration(builder.Configuration,
    (busRegistrationConfigurator) =>
    {
        busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
    },
    (busContext, endpointConfigurator) =>
    {
    });

builder.Services
    .AddHostedService<OrdersBackgroundService>();

builder.Services
    .ConfigureOpenTelemetry(builder.Configuration);

var host = builder.Build();

await host.RunAsync();