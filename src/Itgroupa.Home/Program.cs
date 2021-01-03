using System;

namespace Itgroupa.Home
{
    internal static class Program
    {
        private const string MenuPrint = "start udp-1\n"
                                         +"help-9\n"
                                         +"exit-0\n";
        
        private const string HelpCommand = "9";
        private const string ExitCommand = "0";

        private static void Main()
        {
            var command = HelpCommand;
            while (!string.IsNullOrEmpty(command) &&!command.Equals(ExitCommand))
            {
                switch (command)
                {
                    case HelpCommand:
                        Console.WriteLine(MenuPrint);
                        break;
                }
                command = Console.ReadLine();
            }
        }
    }
}