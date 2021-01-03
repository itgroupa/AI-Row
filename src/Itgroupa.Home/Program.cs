using System;
using Itgroupa.Common;
using Itgroupa.Dto;
using Itgroupa.Udp;

namespace Itgroupa.Home
{
    internal static class Program
    {
        private static readonly IUdpStarter UdpStarter = UdpServerBuilder.Build(Settings.UdpPort);
        private static DataDump _dataDump;

        private static void Main()
        {
            var command = Menu.HelpCommand;
            while (!string.IsNullOrEmpty(command) && !command.Equals(Menu.ExitCommand))
            {
                switch (command)
                {
                    case Menu.HelpCommand:
                        Console.WriteLine(Menu.MenuPrint);
                        break;
                    case Menu.StartUdpCommand:
                        UdpStarter.Start();
                        break;
                    case Menu.StopUdpCommand:
                        _dataDump = UdpStarter.Stop();
                        break;
                    case Menu.SaveDataDumpCommand:
                        FileHelper.SaveToFile(_dataDump, Settings.FileDataDumpName);
                        break;
                    case Menu.LoadDataDumpCommand:
                        _dataDump = FileHelper.GetFromFile<DataDump>(Settings.FileDataDumpName);
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}