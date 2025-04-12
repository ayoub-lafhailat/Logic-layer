using System;
using System.IO;

public class UserInterface
{
    private APIController _apiController;

    public UserInterface()
    {
        _apiController = new APIController();
    }

    public void GetUserInputAndGenerate()
    {
        Console.WriteLine("----- PDF Generator -----");

        int pages = GetIntInput("Enter number of pages: ");
        int pageSize = GetIntInput("Enter target content size per page (in bytes): ");


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