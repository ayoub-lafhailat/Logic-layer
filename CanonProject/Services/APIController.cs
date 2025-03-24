using System;

public class APIController
{
    private PDFGenerator pdfGenerator;

    public APIController()
    {
        pdfGenerator = new PDFGenerator();
    }

    public void HandleRequest(int pages, int pageSize)
    {
        pdfGenerator.GeneratePDF(pages, pageSize);
    }
}
