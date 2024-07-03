using MassTransit;
using MassTransitSample.Extensions;
using MassTransitSample.Sales.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration(builder.Configuration,
    (busRegistrationConfigurator) =>
    {
        busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
    },
    (busContext, endpointConfigurator) =>
    {
        endpointConfigurator.ConfigureConsumer<PlaceOrderConsumer>(busContext);
    });

var host = builder.Build();

await host.RunAsync();