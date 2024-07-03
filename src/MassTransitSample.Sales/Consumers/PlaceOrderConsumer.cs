using MassTransit;
using NServiceBusSample.Contracts;
using NServiceBusSample.Contracts.Events;

namespace MassTransitSample.Sales.Consumers;

public class PlaceOrderConsumer(ILogger<PlaceOrderConsumer> logger) : IConsumer<PlaceOrderCommand>
{
    
    public async Task Consume(ConsumeContext<PlaceOrderCommand> context)
    {
        
        var orderPlacedEvent = new OrderPlacedEvent()
        {
            Id = Guid.NewGuid(),
            OrderId = context.Message.OrderId,
            Description = context.Message.Description,
            ProductId = context.Message.ProductId,
            Version = DateTime.UtcNow
        };
        
        logger.LogInformation("The order with id {OrderId} has been processed", context.Message.OrderId);
        
        await context.Publish(orderPlacedEvent);
        
    }
}