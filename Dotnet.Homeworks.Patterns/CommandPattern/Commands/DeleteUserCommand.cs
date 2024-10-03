using Dotnet.Homeworks.Patterns.CommandPattern.Interfaces;

namespace Dotnet.Homeworks.Patterns.CommandPattern.Commands;

public class DeleteUserCommand : ICommand
{
    private readonly string _username;
    private bool _isExecuted;

    public DeleteUserCommand(string username)
    {
        _username = username;
    }

    public void Execute()
    {
        // Логика
        _isExecuted = true;
        Console.WriteLine($"Пользователь с никнеймом {_username} удален");
    }

    public void Undo()
    {
        if (!_isExecuted)
            throw new ApplicationException(
                "Нельзя отменить команду по удалению пользователя, т.к вы не запустили команду");
        
        // Логика
        _isExecuted = false;
        Console.WriteLine("Пользователь восстановлен");
    }
}