using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.UserManagement.Commands.DeleteUserByAdmin;
using Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;
using Dotnet.Homeworks.Features.Users.Commands.CreateUser;
using Dotnet.Homeworks.Features.Users.Commands.DeleteUser;
using Dotnet.Homeworks.Features.Users.Commands.UpdateUser;
using Dotnet.Homeworks.Features.Users.Queries.GetUser;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;
using GetUserDto = Dotnet.Homeworks.Features.Users.Queries.GetUser.GetUserDto;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public static class TestUser
{
    public static async Task<Result<GetUserDto>> GetUserAsync(Guid guid, IMediator mediator)
    {
        var getCommand = new GetUserQuery(guid);
        return await mediator.Send(getCommand);
    }

    public static async Task<Result<GetAllUsersDto>> GetAllUsersAsync(IMediator mediator)
    {
        var getAllCommand = new GetAllUsersQuery();
        return await mediator.Send(getAllCommand);
    }

    public static async Task<Result> DeleteUserByAdminAsync(Guid guid, IMediator mediator)
    {
        var deleteCommand = new DeleteUserByAdminCommand(guid);
        return await mediator.Send(deleteCommand);
    }

    public static async Task<Result> DeleteUserAsync(Guid guid, IMediator mediator)
    {
        var deleteCommand = new DeleteUserCommand(guid);
        return await mediator.Send(deleteCommand);
    }

    public static async Task<Result> UpdateUserAsync(User user, IMediator mediator)
    {
        var updateCommand = new UpdateUserCommand(user);
        return await mediator.Send(updateCommand);
    }

    public static async Task<Result<CreateUserDto>> CreateUserAsync(string name, string email, IMediator mediator)
    {
        var createCommand = new CreateUserCommand(name, email);
        return await mediator.Send(createCommand);
    }

    public static async Task<Result<CreateUserDto>> CreateUserAsync(IMediator mediator) =>
        await CreateUserAsync(GenerateRandomName(5), GenerateRandomUniqueEmail(), mediator);

    private static string GenerateRandomUniqueEmail() => $"test.{Guid.NewGuid()}@test.tst";

    private static string GenerateRandomName(int length)
    {
        var rnd = GetRandom();
        var randomSequence = new char[length];
        for (var i = 0; i < length; i++)
            randomSequence[i] = LowercaseAlphabet[rnd.Next(LowercaseAlphabet.Length)];
        return new string(randomSequence);
    }

    private static Random GetRandom() => new(DateTime.Now.Nanosecond);

    private static readonly string LowercaseAlphabet = new(Enumerable.Range('a', 26).Select(x => (char)x).ToArray());
}