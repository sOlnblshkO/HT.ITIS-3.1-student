using System.Security.Claims;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;
using Dotnet.Homeworks.Features.Decorators;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker.Enums;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;
using Moq;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.CqrsValidation;

[Collection(nameof(AllUserManagementFixture))]
public class PipelineBehaviorTests
{
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
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi().WithPipelineBehaviors();
        
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });
        
        var env = testEnvBuilder.Build();
        
        // Act
        var result =
            await CqrsEnvironment.HandleQuery<GetAllUsersQuery, GetAllUsersDto>(env.GetAllUsersQueryHandler,
                TestUsers.GetAllUsersQuery());
        
        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnFailedResult_WhenCallGetAllUsersWithNoPermission()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>());
        
        var env = testEnvBuilder.Build();
        
        // Act
        var result =
            await CqrsEnvironment.HandleQuery<GetAllUsersQuery, GetAllUsersDto>(env.GetAllUsersQueryHandler,
                TestUsers.GetAllUsersQuery());
        
        // Assert
        Assert.True(result.IsFailure);
    }
    
    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnFailedResult_WhenCallDeleteUserWithNoPermission()
    {
        // Assert
        var email = "correct@email.ru";
        var name = "name";
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi().WithPipelineBehaviors();
        var guid = await testEnvBuilder.UserRepositoryMock.Object?.InsertUserAsync(new User() {Name = name, Email = email})!;
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>());

        var env = testEnvBuilder.Build();
        
        // Act
        var result =
            await CqrsEnvironment.HandleCommand(env.DeleteUserByAdminCommandHandler,
                TestUsers.DeleteUserByAdminCommand(guid));
        
        // Assert
        Assert.True(result.IsFailure);
        env.UnitOfWorkMock.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task Mediator_Should_InvokeBehaviors_And_ReturnSucceedResult_WhenCallDeleteUser()
    {
        // Assert
        var email = "correct@email.ru";
        var name = "name";
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi().WithPipelineBehaviors();
        var guid = await testEnvBuilder.UserRepositoryMock.Object?.InsertUserAsync(new User() {Name = name, Email = email})!;
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });
        
        var env = testEnvBuilder.Build();
        
        // Act
        var result =
            await CqrsEnvironment.HandleCommand(env.DeleteUserByAdminCommandHandler,
                TestUsers.DeleteUserByAdminCommand(guid));
        
        // Assert
        Assert.True(result.IsSuccess);
        env.UnitOfWorkMock.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
    
    [Homework(RunLogic.Homeworks.CqrsValidatorsDecorators)]
    public async Task DeleteOperation_Should_ReturnFailedResult_WhenNoSuchUserExists()
    {
        // Assert
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi().WithPipelineBehaviors();
        testEnvBuilder.SetupHttpContextClaims(new List<Claim>()
            { new Claim(ClaimTypes.Role, Roles.Admin.ToString()) });
        
        var env = testEnvBuilder.Build();
        
        // Act
       var result =
            await CqrsEnvironment.HandleCommand(env.DeleteUserByAdminCommandHandler,
                TestUsers.DeleteUserByAdminCommand(Guid.NewGuid()));
        
        // Assert
        Assert.True(result.IsFailure);
        env.UnitOfWorkMock.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}