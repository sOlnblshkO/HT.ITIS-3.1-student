using FluentValidation;

namespace Dotnet.Homeworks.Features.Users.Commands.DeleteProfile;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
    }
}