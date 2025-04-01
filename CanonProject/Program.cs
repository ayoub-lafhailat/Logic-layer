using System;

namespace CanonProject;

class Program
{
    static void Main()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        UserInterface ui = new UserInterface();
        ui.GetUserInputAndGenerate();
    }
}


