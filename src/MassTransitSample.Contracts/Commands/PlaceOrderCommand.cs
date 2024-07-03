namespace NServiceBusSample.Contracts;

public class PlaceOrderCommand
{
    
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public string Description { get; set; }
    
    public DateTime Version { get; set; }
    
}