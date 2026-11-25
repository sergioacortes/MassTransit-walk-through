namespace MassTransitSample.Telemetry.Models.OpenTelemetry;

public class OpenTelemetryOptions
{
    public ExportersOptions Exporters { get; set; } = new();

}
