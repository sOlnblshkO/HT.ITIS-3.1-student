using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Repositories;
using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email");
        RuleFor(x => x.Email).NotEmpty().MustAsync(BeUnique).WithMessage("Such email already exists");
    }

    private async Task<bool> BeUnique(string email, CancellationToken token = default)
    {
        var users = await _userRepository.GetUsers();
        var result = users.FirstOrDefault(x=>x.Email == email);
        return result is null;
    }
}