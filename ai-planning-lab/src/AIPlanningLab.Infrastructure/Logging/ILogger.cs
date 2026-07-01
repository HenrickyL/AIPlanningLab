namespace AIPlanningLab.Infrastructure.Logging;

/// <summary>
/// Abstração de logs.
/// </summary>
public interface ILogger
{
    void Info(
        string message
    );

    void Warning(
        string message
    );

    void Error(
        string message
    );
}