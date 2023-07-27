using Dotnet.Homeworks.Storage.API.Configuration;
using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Dotnet.Homeworks.Storage.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MinioConfig>(builder.Configuration.GetSection("MinioConfig"));
builder.Services.AddSingleton<IStorageFactory, StorageFactory>();
builder.Services.AddHostedService<PendingObjectsProcessor>();

var app = builder.Build();
#region Product endpoints

app.MapPost("/products/picture", async ([FromForm] IFormFile formFile, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
{
    var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.ProductsBucket);
    var image = new Image(formFile.OpenReadStream(), formFile.FileName,
        !string.IsNullOrWhiteSpace(formFile.ContentType) ? formFile.ContentType : default);
    var res = await imageStorage.PutItemAsync(image, cancellationToken);
    return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
});

app.MapGet("/products/picture",
    async (string fileName, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
    {
        var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.ProductsBucket);
        var image = await imageStorage.GetItemAsync(fileName, cancellationToken);
        return image.Content is null
            ? Results.NotFound()
            : Results.File(image.Content, image.ContentType, image.FileName);
    });

app.MapDelete("/products/picture",
    async (string fileName, IStorageFactory storageFactory, CancellationToken cancellationToken) =>
    {
        var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.ProductsBucket);
        var res = await imageStorage.RemoveItemAsync(fileName, cancellationToken);
        return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
    });

app.MapGet("/products/pictures", async (IStorageFactory storageFactory, CancellationToken cancellationToken) =>
{
    var imageStorage = await storageFactory.CreateImageStorageWithinBucketAsync(Constants.ProductsBucket);
    var res = await imageStorage.ListItemsAsync(cancellationToken);
    return Results.Ok(res);
});

#endregion

app.Run();