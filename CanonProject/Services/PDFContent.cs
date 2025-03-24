using iText.IO.Image;
using SkiaSharp;
using System;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using System;
using Path = System.IO.Path;
public class PDFContent
{
    private byte[] data;

    public PDFContent(int size)
    {
        data = GenerateImageData(size);
    }

    public static int CalculateExpectedSize(int pages, int pageSize)
    {
        const int PDF_PAGE_OVERHEAD = 256;
        const int PDF_FIXED_OVERHEAD = 1024;
        const float PNG_COMPRESSION_RATIO = 1.02f;
        const float METADATA_MARGIN = 1.03f;

        long estimatedPngSize = (long)(pageSize * PNG_COMPRESSION_RATIO);
        long totalSize = PDF_FIXED_OVERHEAD + (pages * PDF_PAGE_OVERHEAD) + (estimatedPngSize * pages);
        return (int)(totalSize * METADATA_MARGIN);
    }

    private static byte[] GenerateImageData(int targetBytes)
    {
        int pixelCount = targetBytes / 3;
        int dimension = (int)Math.Sqrt(pixelCount);

        using var surface = SKSurface.Create(new SKImageInfo(dimension, dimension));
        using var canvas = surface.Canvas;

        var random = new Random();
        for (int y = 0; y < dimension; y++)
        {
            for (int x = 0; x < dimension; x++)
            {
                var color = new SKColor(
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256)
                );
                using var paint = new SKPaint { Color = color };
                canvas.DrawPoint(x, y, paint);
            }
        }

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }

    public Image GenerateData()
    {
        return new Image(ImageDataFactory.Create(data)).SetAutoScale(true);
    }
}
