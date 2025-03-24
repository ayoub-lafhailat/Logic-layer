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
    private List<PDFPage> pdfPages;

    public void GeneratePDF(int pages, int pageSize)
    {
        this.pages = pages;
        this.pageSize = pageSize;
        this.totalSize = CalculateSize(pages, pageSize);
        this.pdfPages = new List<PDFPage>();

        for (int i = 0; i < pages; i++)
        {
            pdfPages.Add(new PDFPage(595, 842, pageSize / pages));
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
}
