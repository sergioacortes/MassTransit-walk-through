namespace MassTransitSample.Telemetry.Models.Datadog;

public class DatadogOptions
{
    public LogsOptions Logs { get; set; } = new();

    public string ApiKey { get; set; } = string.Empty;
}
