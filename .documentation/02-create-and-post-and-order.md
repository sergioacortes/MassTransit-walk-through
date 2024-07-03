# Create and post and order with LearningTransport

This solution contains the projects used by the MassTransit walk through sample.

- MassTransitSample.Orders, this project is the responsible of post new order on the system.
- MassTransitSample.Sales, this project is responsible of create the order and dispatch the order completed message.
- MassTransitSample.Billing, this project is responsible of react to the order completed message to process the billing.

## Functional requirements

Every 5 seconds a new order is posted and as a consequence of that order a new sale is registered in the system, finally, a billing has to be generated.

## Technical implementation

- The MassTransitSample.Orders microservice has a background service that every 5 second create and send a new PlaceOrderCommand to the MassTransitSample.Sales.
- The MassTransitSample.Sales microservice handle the command PlacerOrderCommand and process it. Once the order is processed an OrderPlacedEvent is publish.
- THe MassTransitSample.Billing microservice handle the event OrderPlacedEvent to generate the corresponding billing.

# Configure MassTransit

The project MassTransitSample.Extensions has been created to include extensions methods to configure MassTransit.

For this example, RabbitMq is used. [Checkout the infrastructure repository](https://github.com/sergioacortes/docker-infrastructure)

## Extension method usage

The Program of every microservice use the extension method to configure MassTransit 

```

...

builder.Services.AddMassTransitConfiguration(builder.Configuration,
    (busRegistrationConfigurator) =>
    {
        busRegistrationConfigurator.AddConsumers(typeof(Program).Assembly);
    },
    (busContext, endpointConfigurator) =>
    {
        endpointConfigurator.ConfigureConsumer<PlaceOrderConsumer>(busContext);
    });

...

```

This extension method does the following

- Configure MassTransit and use RabbitMq as transport
- Add the consumer in the Program assembly
- Use the endpoint configurator to configure the consumer within the bus context

## appsettings.json

The appsettings.json files are modified to include the MassTransit configuration on every microservice

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        }
    },
    "MassTransit": {
        "QueueName": "MassTransitSample.Sales",
        "ErrorQueueName": "MassTransitSample.Errors",
        "RabbitMq": {
            "Url": "amqp://guest:guest@localhost:5672/"
        },
        "MaximumConcurrencyLevel": 40,
        "NumberOfRetries": 2
    }
}
```