using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using System;
using System.IO;
using System.Collections.Generic;
using iText.Layout.Properties;

public class PDFGenerator
{
    private int pages;
    private int pageSize;
    private int totalSize;

    private int Width;

    private int Height;
    private List<PDFPage> pdfPages;

    private enum DocumentSizeFormats
    {
        A0,
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        B0,
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        C0,
        C1,
        C2,
        C3,
        C4,
        C5,
        C6,
        Letter,
        Legal,
        Tabloid,
        Ledger,
        Executive,
        Folio
    }

    public void GeneratePDF(int pages, int pageSize)
    {
        this.pages = pages;
        this.pageSize = pageSize;
        this.totalSize = CalculateSize(pages, pageSize);
        this.pdfPages = new List<PDFPage>();

        var A4Dimensions = GetDocumentSizeFormat("A4");

        if(A4Dimensions == null){
            return;
        }

        for (int i = 0; i < pages; i++)
        {
            pdfPages.Add(new PDFPage(A4Dimensions.Value.Width, A4Dimensions.Value.Height, pageSize / pages));
        }

        SavePDF();
    }

    public void GeneratePDF(int pages, int pageSize, int Width, int Height)
    {
        this.pages = pages;
        this.pageSize = pageSize;
        this.totalSize = CalculateSize(pages, pageSize);
        this.pdfPages = new List<PDFPage>();

        for (int i = 0; i < pages; i++)
        {
            pdfPages.Add(new PDFPage(Width, Height, pageSize / pages));
        }

        SavePDF();
    }

    public void GeneratePDF(int pages, int pageSize, string format)
    {
        this.pages = pages;
        this.pageSize = pageSize;
        this.totalSize = CalculateSize(pages, pageSize);
        this.pdfPages = new List<PDFPage>();

        var DocumentSize = CheckFormat(format);

        if (DocumentSize != null)
        {
            Width = DocumentSize.Value.Width;
            Height = DocumentSize.Value.Height;
        }
        else
        {
            Console.WriteLine("Invalid format");
        }

        for (int i = 0; i < pages; i++)
        {
            pdfPages.Add(new PDFPage(Width, Height, pageSize / pages));
        }

        SavePDF();
    }

    private int CalculateSize(int pages, int pageSize)
    {
        return PDFContent.CalculateExpectedSize(pages, pageSize);
    }

    private void SavePDF()
    {
        string outputDirectory = Path.Combine(Environment.CurrentDirectory, "GeneratedPDF");

        // Ensure the folder exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        string outputPath = Path.Combine(outputDirectory, "output.pdf");

        using var writer = new PdfWriter(outputPath);
        using var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        for (int i = 0; i < pdfPages.Count; i++)
        {
            // Render and add the page's content
            var pageContent = pdfPages[i].RenderContent();
            if (pageContent != null) // Ensure that there's actual content
            {
                document.Add(pageContent);
            }

            // Explicitly force a page break only if this is not the last page
            if (i < pdfPages.Count - 1)
            {
                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE)); // Add page break
            }
        }
    }

    private (int Width, int Height)? CheckFormat(string Format)
    {
        Format = ToCapitalCase(Format);

        var FormatDimension = GetDocumentSizeFormat(Format);

        if (FormatDimension != null)
        {
            int Width = FormatDimension.Value.Width;
            int Height = FormatDimension.Value.Height;
            return (Width, Height);
        }

        return null;
    }

    private string ToCapitalCase(string Input)
    {
        return char.ToUpper(Input[0]) + Input.Substring(1).ToLower();
    }


    private (DocumentSizeFormats Format, int Width, int Height)? GetDocumentSizeFormat(string format)
    {
        if (Enum.TryParse(typeof(DocumentSizeFormats), format, true, out var formatEnum) &&
            formatEnum is DocumentSizeFormats validFormat)
        {
            if (!Enum.IsDefined(typeof(DocumentSizeFormats), validFormat) || int.TryParse(format, out _))
            {
                return null;
            }

            var documentSizes = new Dictionary<DocumentSizeFormats, (int Width, int Height)>
            {
                { DocumentSizeFormats.A0, (841, 1189) },
                { DocumentSizeFormats.A1, (594, 841) },
                { DocumentSizeFormats.A2, (420, 594) },
                { DocumentSizeFormats.A3, (297, 420) },
                { DocumentSizeFormats.A4, (210, 297) },
                { DocumentSizeFormats.A5, (148, 210) },
                { DocumentSizeFormats.A6, (105, 148) },
                { DocumentSizeFormats.B0, (1000, 1414) },
                { DocumentSizeFormats.B1, (707, 1000) },
                { DocumentSizeFormats.B2, (500, 707) },
                { DocumentSizeFormats.B3, (353, 500) },
                { DocumentSizeFormats.B4, (250, 353) },
                { DocumentSizeFormats.B5, (176, 250) },
                { DocumentSizeFormats.B6, (125, 176) },
                { DocumentSizeFormats.C0, (917, 1297) },
                { DocumentSizeFormats.C1, (648, 917) },
                { DocumentSizeFormats.C2, (458, 648) },
                { DocumentSizeFormats.C3, (324, 458) },
                { DocumentSizeFormats.C4, (229, 324) },
                { DocumentSizeFormats.C5, (162, 229) },
                { DocumentSizeFormats.C6, (114, 162) },
                { DocumentSizeFormats.Letter, (216, 279) },
                { DocumentSizeFormats.Legal, (216, 356) },
                { DocumentSizeFormats.Tabloid, (279, 432) },
                { DocumentSizeFormats.Ledger, (432, 279) },
                { DocumentSizeFormats.Executive, (184, 267) },
                { DocumentSizeFormats.Folio, (216, 330) }
            };

            var size = documentSizes[validFormat];
            return (validFormat, size.Width, size.Height);
        }

        return null;
    }
}
