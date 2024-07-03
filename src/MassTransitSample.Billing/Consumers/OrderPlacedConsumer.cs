using MassTransit;
using NServiceBusSample.Contracts.Events;

namespace MassTransitSample.Billing.Consumers;

public class OrderPlacedConsumer(ILogger<OrderPlacedEvent> logger) : IConsumer<OrderPlacedEvent>
{
    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        
        logger.LogInformation("Processing the billing for the order {OrderId}", context.Message.OrderId);

        return Task.CompletedTask;
        
    }
}