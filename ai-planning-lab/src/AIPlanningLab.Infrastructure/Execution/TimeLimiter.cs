using AIPlanningLab.Infrastructure.Metrics;

namespace AIPlanningLab.Infrastructure.Execution;

public class TimeLimiter : IExecutionLimiter
{
    private readonly ITimeMetric _timer;
    private TimeSpan Limit { get; set; }
    public int LimitInMiliseconds => Limit.Milliseconds;


    public TimeLimiter(ITimeMetric timer, int seconds = 60)
    {
        _timer = timer;
        Limit = TimeSpan.FromSeconds(seconds);
    }

    public bool ShouldStop()
        => _timer.Elapsed >= Limit;
}
