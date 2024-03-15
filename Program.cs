





using System;

namespace GitAndDeploy;

// <None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
// var settings = new Settings();
// var settingsSection = configuration.GetSection("Settings");
// settingsSection.Bind(settings);


public class Program
{

    public static void Main(string[] args)
    {

        var ui = UserInterface.Create();

        switch (args.Length)
        {
            case 0: 
            {
                ui.ShowArgList();
                break;
            }
            case 1: 
            {
                Console.WriteLine($"Please set an action and the current git branch as well!\n");
                ui.ShowArgList();
                break;
            }
            case 2: 
            {
                Console.WriteLine($"Please set the current git branch as well!\n");
                ui.ShowArgList();
                break;
            }
            case 3: 
            {
                ui.HandleArgs(args[0], args[1], args[2]);
                break;
            }
            default:
            {
                Console.WriteLine($"These are too many arguments!\n");
                ui.ShowArgList();
                break;
            }

        }
    }
}
