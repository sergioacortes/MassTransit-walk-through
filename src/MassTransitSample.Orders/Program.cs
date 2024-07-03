using MassTransit;
using MassTransitSample.Extensions;
using MassTransitSample.Orders.BackgroundServices;

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

var host = builder.Build();

await host.RunAsync();