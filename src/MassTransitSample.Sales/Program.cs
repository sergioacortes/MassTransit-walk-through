using MassTransit;
using MassTransitSample.Extensions;
using MassTransitSample.Sales.Consumers;
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
            endpointConfigurator.ConfigureConsumer<PlaceOrderConsumer>(busContext);
        });

builder.Services
    .ConfigureOpenTelemetry(builder.Configuration);

var host = builder.Build();

await host.RunAsync();