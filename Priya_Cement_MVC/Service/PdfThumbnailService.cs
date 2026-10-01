using PDFtoImage;
using SkiaSharp;

public class PdfThumbnailService
{
    private readonly HttpClient _httpClient;

    public PdfThumbnailService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<byte[]> CreateThumbnail(string pdfUrl)
    {
        var pdfBytes = await _httpClient.GetByteArrayAsync(pdfUrl);

        using var stream = new MemoryStream(pdfBytes);

        using var bitmap = Conversion.ToImage(stream, page: 0);

        using var image = SKImage.FromBitmap(bitmap);

        using var data = image.Encode(
            SKEncodedImageFormat.Jpeg, 80);

        return data.ToArray();
    }
}