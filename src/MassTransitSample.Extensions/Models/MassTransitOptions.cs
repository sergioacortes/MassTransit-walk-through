namespace MassTransitSample.Extensions.Models;

public class MassTransitOptions
{

    public const string DefaultSectionKey = "MassTransit";

    public string QueueName { get; init; } = string.Empty;

    public string ErrorQueueName { get; init; } = string.Empty;

    public int MaximumConcurrencyLevel { get; init; } = 0;

    public int NumberOfRetries { get; init; } = 2;

    public MassTransitRabbitMqOptions RabbitMq { get; init; } = new();

}