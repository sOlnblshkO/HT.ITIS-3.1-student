using Dotnet.Homeworks.Storage.API.Configuration;
using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Dotnet.Homeworks.Storage.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.Storage.API.Endpoints;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        app.MapPost("/products/picture", async ([FromForm] IFormFile formFile, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
        {
            var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.Buckets.Products);
            var image = new Image(formFile.OpenReadStream(), formFile.FileName,
                !string.IsNullOrWhiteSpace(formFile.ContentType) ? formFile.ContentType : default);
            var res = await imageStorage.PutItemAsync(image, cancellationToken);
            return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
        });

        app.MapGet("/products/picture",
            async (string fileName, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
            {
                var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.Buckets.Products);
                var image = await imageStorage.GetItemAsync(fileName, cancellationToken);
                return image.Content is null
                    ? Results.NotFound()
                    : Results.File(image.Content, image.ContentType, image.FileName);
            });

        app.MapDelete("/products/picture",
            async (string fileName, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
            {
                var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.Buckets.Products);
                var res = await imageStorage.RemoveItemAsync(fileName, cancellationToken);
                return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
            });

        app.MapGet("/products/pictures", async (IStorageFactory storageFactory, CancellationToken cancellationToken) =>
        {
            var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.Buckets.Products);
            var res = await imageStorage.EnumerateItemNamesAsync(cancellationToken);
            return Results.Ok(res);
        });
    }
}