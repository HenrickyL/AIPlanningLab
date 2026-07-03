using AIPlanningLab.Application.Heuristics;
using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Services;
using AIPlanningLab.Infrastructure.Execution;
using AIPlanningLab.Infrastructure.Metrics;

namespace AIPlanningLab.Implementations.Explicit.Heuristics;

/// <summary>
/// h(s) = distância exata (nº de ações) de s até a meta,
/// </summary>
public sealed class BackwardDistanceHeuristic : IPrecomputedHeuristic
{
    private readonly IPlanningOperator _operator;
    private Dictionary<ExplicitState, int>? _distance;
    private int _maxValue = int.MinValue;
    private readonly ITimeMetric _timeMetric;
    private readonly IExecutionLimiter _execLimiter;

    public BackwardDistanceHeuristic(
        IPlanningOperator planningOperator,
        ITimeMetric timeMetric,
        IExecutionLimiter execLimiter
    )
    {
        _operator = planningOperator;
        _timeMetric = timeMetric;
        _execLimiter = execLimiter;
    }

    public int Evaluate(IState state, IPlanningProblem problem)
    {
        if (_distance is null)
            throw new InvalidOperationException(
                "Precompute() precisa ser chamado antes de Evaluate() — ver ForwardPlanner.Solve.");

        var explicitState = (ExplicitState)state;
        return _distance.TryGetValue(explicitState, out var d) ? d : _maxValue+1;
    }

    public void Precompute(IPlanningProblem problem)
    {
        Console.WriteLine("Start Precompute...");
        _timeMetric.Restart();

        var distance = new Dictionary<ExplicitState, int>();
        var frontier = new PriorityQueue<ExplicitState, int>();

        var goal = (ExplicitState)problem.Goal;
        distance[goal] = 0;
        frontier.Enqueue(goal, 0);
        ExplicitState current;
        IState initial = problem.InitialState;

        while (frontier.Count > 0)
        {
            if (_execLimiter.ShouldStop())
            {
                _timeMetric.Save("Precompute.Timeout");
                Console.WriteLine($">TIME_LIMIT {_execLimiter.LimitInMiliseconds}");
                break;
            }

            current = frontier.Dequeue();
            int currentCost = distance[current];

            // Lazy deletion
            if (currentCost > distance.GetValueOrDefault(current, int.MaxValue))
                continue;
            if (initial.Satisfies(current)) {
                break;
            }

            foreach (var action in problem.Domain.Actions)
            {
                if (!_operator.CanRelaxedRegress(current, action))
                    continue;

                var predecessor = (ExplicitState)_operator.RelaxedRegress(current, action);
                int newCost = currentCost + action.Cost;

                if (!distance.TryGetValue(predecessor, out var knownCost) ||
                    newCost < knownCost)
                {
                    distance[predecessor] = newCost;
                    _maxValue = Math.Max(_maxValue, newCost);
                    frontier.Enqueue(predecessor, newCost);
                }
            }
        }

        _distance = distance;

        _timeMetric.Save("Precompute.End");
        _timeMetric.Restart();

        Console.WriteLine($"Precompute end --> Dict:{distance.Count}");
    }
}
