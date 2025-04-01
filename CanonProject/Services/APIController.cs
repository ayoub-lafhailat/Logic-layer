using System;

public class APIController
{
    private PDFGenerator _pdfGenerator;

    public APIController()
    {
        _pdfGenerator = new PDFGenerator();
    }

    public long HandleRequest(int pages, int pageSize)
    {
        try
        {
            _pdfGenerator.Configure(pages, pageSize);
            long actualSize = _pdfGenerator.GenerateAndSavePDF();
            return actualSize;
        }
        catch (ArgumentOutOfRangeException argEx)
        {
            Console.WriteLine($"Configuration Error: {argEx.ParamName} - {argEx.Message}");
            return -1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during PDF generation request: {ex.Message}");
            return -1;
        }
    }

    public long GetEstimatedSize()
    {
        return _pdfGenerator?.EstimatedTotalSizeBytes ?? 0;
    }
}