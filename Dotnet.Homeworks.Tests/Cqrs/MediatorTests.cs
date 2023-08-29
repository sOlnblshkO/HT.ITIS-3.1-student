using Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.Cqrs;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.Cqrs;

[Collection(nameof(AllProductsRequestsFixture))]
public class MediatorTests
{
    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public void MediatR_Should_ResideInMainProject()
    {
        var result = MainProject.Helpers.AssemblyReference.Assembly
            .GetReferencedAssemblies()
            .FirstOrDefault(x => x.Name == Constants.MediatR);

        Assert.NotNull(result);
    }

    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task Controller_Should_CallMediatR_WhenCallGetProducts()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.GetProducts(CancellationToken.None);

        await env.MediatR.Received().Send(Arg.Any<IQuery<GetProductsDto>>(), Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task Controller_Should_CallMediatR_WhenCallInsertProduct()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.InsertProduct("Name", CancellationToken.None);

        await env.MediatR.Received().Send(Arg.Any<ICommand<InsertProductDto>>(), Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task Controller_Should_CallMediatR_WhenCallDeleteProduct()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.DeleteProduct(Guid.NewGuid(), CancellationToken.None);

        await env.MediatR.Received().Send(Arg.Any<DeleteProductByGuidCommand>(), Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task Controller_Should_CallMediatR_WhenCallUpdateProduct()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.UpdateProduct(Guid.NewGuid(), "Name", CancellationToken.None);

        await env.MediatR.Received().Send(Arg.Any<UpdateProductCommand>(), Arg.Any<CancellationToken>());
    }
}