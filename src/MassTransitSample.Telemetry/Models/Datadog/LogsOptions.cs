namespace MassTransitSample.Telemetry.Models.Datadog;

public class LogsOptions
{
    public bool Enabled { get; set; }

    public Uri? Endpoint { get; set; }

}
