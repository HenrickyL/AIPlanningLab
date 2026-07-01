using AIPlanningLab.Domain.Models;

namespace AIPlanningLab.Infrastructure.Serialization;

/// <summary>
/// Converte estados para texto.
/// </summary>
public interface IStateSerializer
{
    string Serialize(IState state);
}