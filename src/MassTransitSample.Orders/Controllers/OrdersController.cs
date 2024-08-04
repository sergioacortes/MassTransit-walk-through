using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NServiceBusSample.Contracts;

namespace MassTransitSample.Orders.Controllers;

[Route("api/[controller]")]
public class OrdersController(IBus bus, ILogger<OrdersController> logger) : ControllerBase
{
    
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
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
        
        return Ok();
    }
    
}