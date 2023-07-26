using Dotnet.Homeworks.Storage.API.Configuration;
using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Dotnet.Homeworks.Storage.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MinioConfig>(builder.Configuration.GetSection("MinioConfig"));
builder.Services.AddSingleton<IImageStorageFactory, ImageStorageFactory>();

var app = builder.Build();

#region Product endpoints

app.MapPost("/products/picture", async ([FromForm] IFormFile formFile, IImageStorageFactory storageFactory, CancellationToken cancellationToken) =>
{
    var imageStorage = await storageFactory.CreateWithinBucketAsync(Constants.ProductsBucket);
    var image = new Image(formFile.OpenReadStream(), formFile.FileName,
        !string.IsNullOrWhiteSpace(formFile.ContentType) ? formFile.ContentType : default);
    var res = await imageStorage.PutObjectAsync(image, cancellationToken);
    return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
});

app.MapGet("/products/picture",
    async (string fileName, IImageStorageFactory storageFactory, CancellationToken cancellationToken) =>
    {
        var imageStorage = await storageFactory.CreateWithinBucketAsync(Constants.ProductsBucket);
        var image = await imageStorage.GetObjectAsync(fileName, cancellationToken);
        return image.Content is null
            ? Results.NotFound()
            : Results.File(image.Content, image.ContentType, image.FileName);
    });

app.MapDelete("/products/picture",
    async (string fileName, IImageStorageFactory storageFactory, CancellationToken cancellationToken) =>
    {
        var imageStorage = await storageFactory.CreateWithinBucketAsync(Constants.ProductsBucket);
        var res = await imageStorage.RemoveObjectAsync(fileName, cancellationToken);
        return res.IsSucceeded ? Results.Ok() : Results.BadRequest(res.ResultMessage);
    });

app.MapGet("/products/pictures", async (IImageStorageFactory storageFactory, CancellationToken cancellationToken) =>
{
    var imageStorage = await storageFactory.CreateWithinBucketAsync(Constants.ProductsBucket);
    var res = await imageStorage.ListObjectsAsync(cancellationToken);
    return Results.Ok(res);
});

#endregion

app.Run();