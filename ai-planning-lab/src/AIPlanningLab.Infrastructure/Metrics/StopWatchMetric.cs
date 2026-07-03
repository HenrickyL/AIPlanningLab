using System.Diagnostics;

namespace AIPlanningLab.Infrastructure.Metrics;
public sealed class StopwatchMetric : ITimeMetric
{
    private readonly Stopwatch _stopwatch = new();
    private readonly Dictionary<string, TimeSpan> _saveData = new();

    public void Start() {
        _saveData.Clear();
        _stopwatch.Stop();
        _stopwatch.Start();
    } 

    public void Restart() => _stopwatch.Restart();

    public void Stop() => _stopwatch.Stop();

    public void Save(string key)
    {
        _saveData[key] = Elapsed;
    }

    public int? GetMiliseconds(string key)
    {
        if (_saveData.TryGetValue(key, out var value))
        {
            return (int)value.TotalMilliseconds;
        }
        else
            return null;
    }

    public TimeSpan Elapsed => _stopwatch.Elapsed;

    public double ElapsedMilliseconds => _stopwatch.Elapsed.TotalMilliseconds;
}
