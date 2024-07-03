using MassTransit;
using MassTransitSample.Billing.Consumers;
using MassTransitSample.Extensions;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddMassTransitConfiguration(builder.Configuration,
    (busRegistrationConfigurator) =>
    {
        busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
    },
    (busContext, endpointConfigurator) =>
    {
        endpointConfigurator.ConfigureConsumer<OrderPlacedConsumer>(busContext);
    });

var host = builder.Build();

await host.RunAsync();