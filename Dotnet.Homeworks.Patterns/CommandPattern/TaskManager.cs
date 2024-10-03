using Dotnet.Homeworks.Patterns.CommandPattern.Interfaces;

namespace Dotnet.Homeworks.Patterns.CommandPattern;

public class TaskManager
{
    private readonly Stack<ICommand> _commands = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commands.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commands.Count == 0)
            throw new ApplicationException("На данный момент нет задач, которых можно отменить");

        var command = _commands.Pop();
        command.Undo();
    }
}