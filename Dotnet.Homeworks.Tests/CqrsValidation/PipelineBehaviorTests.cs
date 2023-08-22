using System.Security.Claims;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Validation.Decorators;
using Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker.Enums;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.Cqrs;
using Dotnet.Homeworks.Tests.Shared.TestRequests;
using Moq;
using NetArchTest.Rules;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.CqrsValidation;

[Collection(nameof(AllUserManagementFixture))]
public class PipelineBehaviorTests
{
    private const string Email = "correct@email.ru";
    private const string Name = "Name";

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public void RequestHandlers_ShouldNot_InheritDecorators()
    {
        var typesWithCondition = Types.InAssembly(Features.Helpers.AssemblyReference.Assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .And()
            .ResideInNamespaceStartingWith(Constants.UserManagementFeatureNamespace);

        var result = typesWithCondition
            .ShouldNot()
            .Inherit(typeof(CqrsDecorator<,>))
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnSucceedResult_WhenCallGetAllUsers()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithPipelineBehaviors();

        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });

        var env = testEnvBuilder.Build();

        // Act
        var result = await TestUser.GetAllUsersAsync(env.CustomMediator);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnFailedResult_WhenCallGetAllUsersWithNoPermission()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>());

        var env = testEnvBuilder.Build();

        // Act
        var result = await TestUser.GetAllUsersAsync(env.CustomMediator);

        // Assert
        Assert.True(result.IsFailure);
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnFailedResult_WhenCallDeleteUserWithNoPermission()
    {
        // Assert;
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>());

        var env = testEnvBuilder.Build();
        var guid = await env.UserRepository.InsertUserAsync(new User() { Name = Name, Email = Email });

        // Act
        var result = await TestUser.DeleteUserByAdminAsync(guid, env.CustomMediator);

        // Assert
        Assert.True(result.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(It.IsAny<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnSucceedResult_WhenCallDeleteUser()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });

        var env = testEnvBuilder.Build();
        var guid = await env.UserRepository.InsertUserAsync(new User() { Name = Name, Email = Email });

        // Act
        var result = await TestUser.DeleteUserByAdminAsync(guid, env.CustomMediator);

        // Assert
        Assert.True(result.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(It.IsAny<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task DeleteOperation_Should_ReturnFailedResult_WhenNoSuchUserExists()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });

        var env = testEnvBuilder.Build();

        // Act
        var result = await TestUser.DeleteUserByAdminAsync(Guid.NewGuid(), env.CustomMediator);

        // Assert
        Assert.True(result.IsFailure);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(It.IsAny<CancellationToken>());
    }
}