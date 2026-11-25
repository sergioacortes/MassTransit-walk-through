using MassTransit;
using MassTransitSample.Billing.Consumers;
using MassTransitSample.Extensions;
using MassTransitSample.Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMassTransitConfiguration(builder.Configuration,
        (busRegistrationConfigurator) =>
        {
            busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
        },
        (busContext, endpointConfigurator) =>
        {
            endpointConfigurator.ConfigureConsumer<OrderPlacedConsumer>(busContext);
        });

builder.Services
    .ConfigureOpenTelemetry(builder.Configuration);

var host = builder.Build();

await host.RunAsync();