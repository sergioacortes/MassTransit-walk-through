namespace NServiceBusSample.Contracts.Events;

public class OrderPlacedEvent
{
    
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public string Description { get; set; }
    
    public DateTime Version { get; set; }
    
}