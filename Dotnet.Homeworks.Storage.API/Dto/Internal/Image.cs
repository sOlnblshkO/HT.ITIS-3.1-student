using Microsoft.AspNetCore.StaticFiles;

namespace Dotnet.Homeworks.Storage.API.Dto.Internal;

public record Image
{
    public Stream Content { get; }
    public string FileName { get; }
    public string ContentType { get; }
    public Dictionary<string, string> Metadata { get; }
    
    public Image(Stream content, string fileName, string? contentType = default,
        Dictionary<string, string>? metadata = default)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType ?? GetContentType(fileName);
        Metadata = metadata ?? new Dictionary<string, string>();
    }
    
    private static string GetContentType(string fileName)
    {
        return new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType)
            ? contentType
            : "application/octet-stream";
    }
}