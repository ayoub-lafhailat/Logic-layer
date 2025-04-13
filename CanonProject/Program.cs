using System;

namespace CanonProject;

class Program
{
    static void Main()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        UserInterface ui = new UserInterface();
        
        while (true)
        {
            Console.WriteLine("----- PDF Generator -----");
            Console.WriteLine("1. Generate Single PDF");
            Console.WriteLine("2. Generate Multiple PDFs");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        ui.GenerateSingle();
                        break;
                    case 2:
                        ui.GenerateMultiple();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}


