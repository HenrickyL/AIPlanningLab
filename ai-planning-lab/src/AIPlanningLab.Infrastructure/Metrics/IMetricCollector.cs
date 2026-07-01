namespace AIPlanningLab.Infrastructure.Metrics;

/// <summary>
/// Coleta métricas durante execução.
/// </summary>
public interface IMetricCollector
{
    void Start();

    void Stop();

    MetricSnapshot Snapshot();
}

public sealed class MetricSnapshot
{
    public TimeSpan Time { get; }

    public long Memory { get; }
}