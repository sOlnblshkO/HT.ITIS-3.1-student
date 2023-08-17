using System.Security.Claims;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Decorators;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;
using Moq;
using NetArchTest.Rules;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.CqrsValidation;

[Collection(nameof(AllUsersRequestsFixture))]
public class DecoratorTests
{
    const string Email = "correct@email.ru";
    const string Name = "name";
    
    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public void RequestHandlers_Should_InheritDecorators()
    {
        var typesWithCondition = Types.InAssembly(Features.Helpers.AssemblyReference.Assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .And()
            .ResideInNamespaceStartingWith(Constants.UsersFeatureNamespace);

        var result = typesWithCondition
            .Should()
            .Inherit(typeof(CqrsDecorator<,>))
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [InlineData("wrongemail.ru", "Name")]
    [InlineData("wrong@ email.ru", "Name")]
    [InlineData("w{ron~g@_emai]l.ru", "Name")]
    [InlineData("", "Name")]
    [InlineData(null, "Name")]
    [HomeworkTheory(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task CreateUserOperation_Must_ReturnFailedResult_WhenEmailIsInvalid(string email, string name)
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.CreateUserCommand(name: name, email: email));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task CreateUserOperation_Must_ReturnFailedResult_WhenEmailIsNotUnique()
    {
        // Arrange
        string email = "copy@email.ru";
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();

        var env = testEnvBuilder.Build();
        await env.UserRepository.InsertUserAsync(new User() { Name = Name, Email = email });

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.CreateUserCommand(name: Name, email: email));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [InlineData(Email, Name)]
    [HomeworkTheory(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task CreateUserOperation_Should_ReturnSucceedResult_WhenEmailIsCorrect(string email, string name)
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.CreateUserCommand(name: name, email: email));

        // Assert
        Assert.True(result?.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task GetUserOperation_Must_ReturnFailedResult_WhenUserHasNoPermission()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) });

        var env = testEnvBuilder.Build();
        var guid = await env.UserRepository.InsertUserAsync(new User() { Name = Name, Email = Email });

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.GetUserQuery(guid));

        // Assert
        Assert.True(result?.IsFailure);
    }

    [InlineData(Email, Name)]
    [HomeworkTheory(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task GetUserOperation_Must_ReturnSucceedResult_WhenRequestIsValid(string email, string name)
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var guid = Guid.NewGuid();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, guid.ToString()) });

        var env = testEnvBuilder.Build();
        await env.UserRepository.InsertUserAsync(new User() { Id = guid, Name = name, Email = email })!;

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.GetUserQuery(guid));

        // Assert
        Assert.True(result?.IsSuccess);
    }

    [InlineData(Email, Name)]
    [HomeworkTheory(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task DeleteUserOperation_Must_ReturnSucceedResult_WhenRequestIsValid(string email, string name)
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var guid = Guid.NewGuid();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, guid.ToString()) });

        var env = testEnvBuilder.Build();
        await env.UserRepository.InsertUserAsync(new User() { Id = guid, Name = name, Email = email });

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.DeleteUserCommand(guid));

        // Assert
        Assert.True(result?.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task DeleteUserOperation_Must_ReturnFailedResult_WhenUserHasNoPermission()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var guid = Guid.NewGuid();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) });

        var env = testEnvBuilder.Build();
        await env.UserRepository.InsertUserAsync(new User() { Id = guid, Name = Name, Email = Email })!;

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.DeleteUserCommand(guid));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [InlineData(Email, Name)]
    [HomeworkTheory(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task UpdateUserOperation_Must_ReturnSucceedResult_WhenRequestIsValid(string email, string name)
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var guid = Guid.NewGuid();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, guid.ToString()) });

        var env = testEnvBuilder.Build();
        await env.UserRepository.InsertUserAsync(new User() { Id = guid, Name = name, Email = email });
        var user = new User() { Email = Email, Id = guid, Name = Name };

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.UpdateUserCommand(user));

        // Assert
        Assert.True(result?.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task UpdateUserOperation_Must_ReturnFailedResult_WhenUserHasNoPermission()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var guid = Guid.NewGuid();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) });

        var env = testEnvBuilder.Build();
        await testEnvBuilder.UserRepositoryMock.InsertUserAsync(new User() { Id = guid, Name = Name, Email = Email });
        var user = new User() { Email = Email, Id = guid, Name = Name };

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.UpdateUserCommand(user));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task DeleteOperation_Should_ReturnFailedResult_WhenNoSuchUserExists()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) });

        var env = testEnvBuilder.Build();

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.DeleteUserCommand(Guid.NewGuid()));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task UpdateOperation_Should_ReturnFailedResult_WhenNoSuchUserExists()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) });

        var env = testEnvBuilder.Build();
        var user = new User() { Email = Email, Id = Guid.NewGuid(), Name = Name };

        // Act
        var result =
            await env.CustomMediatorMock.Send(TestUsers.UpdateUserCommand(user));

        // Assert
        Assert.True(result?.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}