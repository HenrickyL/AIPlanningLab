
namespace AIPlanningLab.Infrastructure.Metrics;
public interface ITimeMetric : IMetricCollector
{
    TimeSpan Elapsed { get; }
    void Restart();
    void Save(string key);
    int? GetMiliseconds(string key);
    double ElapsedMilliseconds { get; }
}

