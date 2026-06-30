using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Domain.Services;

/// <summary>
/// Define como ações atuam sobre estados.
/// </summary>
public interface IPlanningOperator
{
    /// <summary>
    /// Verifica aplicabilidade.
    /// </summary>
    bool CanApply(
        IState state,
        IAction action
    );

    /// <summary>
    /// Aplica progressão.
    /// </summary>
    IState Progress(
        IState state,
        IAction action
    );

    /// <summary>
    /// Aplica regressão.
    /// </summary>
    IState Regress(
        IState target,
        IAction action
    );

    /// <summary>
    /// Progressão relaxada.
    /// </summary>
    IState RelaxedProgress(
        IState state,
        IAction action
    );

    /// <summary>
    /// Regressão relaxada.
    /// </summary>
    IState RelaxedRegress(
        IState state,
        IAction action
    );
}