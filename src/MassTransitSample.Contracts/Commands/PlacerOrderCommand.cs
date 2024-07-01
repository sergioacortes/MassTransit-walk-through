namespace NServiceBusSample.Contracts;

public class PlacerOrderCommand
{
    
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public string Description { get; set; }
    
}