using Dotnet.Homeworks.Features.Cqrs.Users.Commands.CreateUser;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Tests.Shared.CqrsStuff;

public static class TestUser
{
    public static CreateUserCommand GetCreateCommand() => new(GenerateRandomName(5), GenerateRandomEmail());
    
    public static async Task<Result<CreateUserDto>> CreateUserAsync(IMediator mediator)
    {
        var createUser = GetCreateCommand();
        return await mediator.Send(createUser);
    }

    private static string GenerateRandomEmail() =>
        "test." + Guid.NewGuid() + "@test.tst";

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