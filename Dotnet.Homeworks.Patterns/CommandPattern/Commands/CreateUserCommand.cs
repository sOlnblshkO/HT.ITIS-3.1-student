using Dotnet.Homeworks.Patterns.CommandPattern.Interfaces;

namespace Dotnet.Homeworks.Patterns.CommandPattern.Commands;

public class CreateUserCommand : ICommand
{
    private readonly string _username;
    private bool _isExecuted;

    public CreateUserCommand(string username)
    {
        _username = username;
    }

    public void Execute()
    {
        // Логика
        _isExecuted = true;
        Console.WriteLine($"Пользователь создан с ником {_username}");
    }

    public void Undo()
    {
        if (!_isExecuted)
            throw new ApplicationException(
                "Нельзя отменить команду по созданию пользователя, т.к вы не запустили команду");

        // Логика
        _isExecuted = false;
        Console.WriteLine("Пользователь удален");
    }
}