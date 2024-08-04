using MassTransit;
using MassTransitSample.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMassTransitConfiguration(builder.Configuration,
    (busRegistrationConfigurator) =>
    {
        busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
    },
    (busContext, endpointConfigurator) =>
    {
    });

var app = builder.Build();

app.MapControllers();
app.UseRouting();

await app.RunAsync();