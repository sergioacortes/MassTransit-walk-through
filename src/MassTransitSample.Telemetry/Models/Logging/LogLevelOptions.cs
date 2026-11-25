using Microsoft.Extensions.Logging;

namespace MassTransitSample.Telemetry.Models.Logging;

public class LogLevelOptions
{
    public LogLevel Default { get; set; } = LogLevel.Information;

}
