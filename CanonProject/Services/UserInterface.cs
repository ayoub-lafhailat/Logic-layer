using System;
using System.IO;

public class UserInterface
{
    private APIController _apiController;

    public UserInterface()
    {
        _apiController = new APIController();
    }

    public void GenerateSingle()
    {
        Console.WriteLine("----- PDF Generator -----");

        int pages = GetIntInput("Enter number of pages: ");
        int pageSize = GetIntInput("Enter target content size per page (in MB): ") * 1024 * 1024; // Convert MB to bytes


        if (pages > 0 && pageSize > 0)
        {
            long actualSize = _apiController.HandleRequest(pages, pageSize);

            if (actualSize < 0)
            {
                Console.WriteLine("\nPDF generation process failed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input provided.");
        }
        Console.WriteLine("\nPress Enter to exit.");
        Console.ReadLine();
    }

    public void GenerateMultiple()
    {
        APIController controller = new APIController();

        Console.WriteLine("----- Batch PDF Generation -----");
        int numberOfFiles = GetIntInput("Enter number of files to generate: ");
        int pagesPerFile = GetIntInput("Enter number of pages per file: ");
        int targetFileSizeBytes = GetIntInput("Enter target file size in MB: ") * 1024 * 1024; // Convert MB to bytes

        // Create base GeneratedPDFs directory if it doesn't exist
        string baseDir = Path.Combine(Environment.CurrentDirectory, "GeneratedPDFs");
        Directory.CreateDirectory(baseDir);

        // Create batch directory inside GeneratedPDFs
        string batchDir = Path.Combine(baseDir, $"Batch_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}");
        Directory.CreateDirectory(batchDir);
        
        // Temporarily modify current directory for batch generation
        string originalDir = Environment.CurrentDirectory;
        Environment.CurrentDirectory = batchDir;

        long[] results = controller.HandleBatchRequest(numberOfFiles, pagesPerFile, targetFileSizeBytes);

        // Restore original directory
        Environment.CurrentDirectory = originalDir;

        Console.WriteLine("\nBatch Summary:");
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"File {i + 1}: {results[i] / 1024.0 / 1024.0:F2} MB ({results[i]} bytes)");
        }

        Console.WriteLine("\nAll files generated.");
    }

private int GetIntInput(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
            Console.Write(prompt);
        }
        return value;
    }
}