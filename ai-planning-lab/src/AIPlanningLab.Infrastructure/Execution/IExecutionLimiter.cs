namespace AIPlanningLab.Infrastructure.Execution;

public interface IExecutionLimiter
{
    int LimitInMiliseconds { get; }
    bool ShouldStop();
}
