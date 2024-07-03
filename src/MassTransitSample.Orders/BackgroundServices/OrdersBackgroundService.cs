using MassTransit;
using NServiceBusSample.Contracts;

namespace MassTransitSample.Orders.BackgroundServices;

public class OrdersBackgroundService(IBus bus, ILogger<OrdersBackgroundService> logger) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {

            var placerOrderCommand = new PlaceOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Description = $"New order",
                ProductId = Guid.NewGuid(),
                Version = DateTime.Now
            };
            
            logger.LogInformation("Sending a new order with id {OrderId}", placerOrderCommand.OrderId);

            await bus.Publish(placerOrderCommand, cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            
        }
    }
}