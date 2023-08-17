using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public static class TestImage
{
    public static Image GetTestImage()
    {
        var content = new MemoryStream(GetRandomDataArray(10));
        return new Image(content, Guid.NewGuid().ToString());
    }

    public static bool ImagesEqual(Image img1, Image img2)
    {
        if (img1.FileName != img2.FileName || img1.ContentType != img2.ContentType ||
            img1.Content.Length != img2.Content.Length) return false;
        img1.Content.Position = 0;
        img2.Content.Position = 0;
        int byte1 = 0, byte2 = 0;
        while (byte1 != -1 && byte2 != -1)
        {
            byte1 = img1.Content.ReadByte();
            byte2 = img2.Content.ReadByte();

            if (byte1 != byte2)
                return false;
        }

        return true;
    }

    private static byte[] GetRandomDataArray(int length)
    {
        var res = new byte[length];
        new Random(DateTime.Now.Nanosecond).NextBytes(res);
        return res;
    }
}