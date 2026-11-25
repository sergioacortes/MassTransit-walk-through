using System.ComponentModel.DataAnnotations;
using MassTransitSample.Telemetry.Models.Datadog;
using MassTransitSample.Telemetry.Models.Logging;
using MassTransitSample.Telemetry.Models.OpenTelemetry;

namespace MassTransitSample.Telemetry.Models;

public class TelemetryOptions
{
    [Required] 
    public string Environment { get; set; } = string.Empty;

    public LoggingOptions Logging { get; set; } = new();

    public DatadogOptions Datadog { get; set; } = new();

    public OpenTelemetryOptions OpenTelemetry { get; set; } = new();

    public string ServiceName { get; set; }  = string.Empty;
    
    public string ServiceVersion { get; set; }  = string.Empty;

}
