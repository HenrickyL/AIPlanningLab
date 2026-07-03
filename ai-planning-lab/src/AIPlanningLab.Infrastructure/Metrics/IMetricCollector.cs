namespace AIPlanningLab.Infrastructure.Metrics;

/// <summary>
/// Coleta métricas durante execução.
/// </summary>
public interface IMetricCollector
{
    void Start();

    void Stop();
}
