using System;

public class UserInterface
{
    private APIController apiController;

    public UserInterface()
    {
        apiController = new APIController();
    }

    public void GetUserInput()
    {
        Console.Write("Enter number of pages: ");
        int pages = int.Parse(Console.ReadLine());

        Console.Write("Enter size per page in bytes: ");
        int pageSize = int.Parse(Console.ReadLine());

        apiController.HandleRequest(pages, pageSize);
        DisplayOutput();
    }

    public void DisplayOutput()
    {
        Console.WriteLine("PDF generated successfully.");
    }
}
