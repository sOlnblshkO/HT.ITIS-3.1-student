using Microsoft.AspNetCore.StaticFiles;

namespace Dotnet.Homeworks.Storage.API.Dto.Internal;

public class Image
{
    public Stream? Content { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    
    public Image(Stream? content, string fileName, string? contentType = default, Dictionary<string, string>? metadata = default)
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