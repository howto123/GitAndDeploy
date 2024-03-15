





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
        var testArgs = new string[] {"Project 2", "Action_Name_2", "mycomment"};

        var ui = UserInterface.Create();

        switch (testArgs.Length)
        {
            case 0: 
            {
                ui.ShowArgList();
                break;
            }
            case 1: 
            {
                Console.WriteLine($"Please set an action and a comment as well!\n");
                ui.ShowArgList();
                break;
            }
            case 2: 
            {
                Console.WriteLine($"Please set a comment as well!\n");
                ui.ShowArgList();
                break;
            }
            case 3: 
            {
                ui.HandleArgs(testArgs[0], testArgs[1], testArgs[2]);
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
