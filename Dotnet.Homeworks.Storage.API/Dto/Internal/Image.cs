using Microsoft.AspNetCore.StaticFiles;

namespace Dotnet.Homeworks.Storage.API.Dto.Internal;

public record Image
{
    public Stream? Content { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    
    public Image(Stream? content, string fileName, string? contentType = default)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType ?? GetContentType(fileName);
    }
    
    private static string GetContentType(string fileName)
    {
        return new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType)
            ? contentType
            : "application/octet-stream";
    }
}