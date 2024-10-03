namespace Dotnet.Homeworks.Patterns.CommandPattern.Interfaces;

/// <summary>
/// Команда
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Выполнить
    /// </summary>
    void Execute();

    /// <summary>
    /// Откатить
    /// </summary>
    void Undo();
}