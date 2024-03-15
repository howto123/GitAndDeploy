

using System;
using System.Management.Automation;
using System.Text;

namespace PwShell;

// From: https://www.youtube.com/watch?v=KUh-RlBcfI8
// We need: System.Management.Automation and Microsoft.PowerShell.SDK

class PowerShellHandler
{
    private readonly PowerShell _shell = PowerShell.Create();


    public void Execute(string script)
    {
        var errorMsg = string.Empty;

        // subscribe errorMsg
        _shell.Streams.Error.DataAdded += (object sender, DataAddedEventArgs e) =>
            errorMsg = ((PSDataCollection<ErrorRecord>)sender)[e.Index].ToString();

        AddScriptToShell(script);

        var output = ExecuteAndGetResultString();

        ClearCommands();
        PrintOutput(errorMsg, output);
    }


    private void AddScriptToShell(string script)
    {
        _shell.AddScript(script);
        _shell.AddCommand("Out-String");
    }


    private string ExecuteAndGetResultString()
    {
        var outputCollection = new PSDataCollection<PSObject>();

        IAsyncResult result = _shell.BeginInvoke<PSObject, PSObject>(null, outputCollection);
        _shell.EndInvoke(result);

        StringBuilder sb = new();
        foreach (var outputItem in outputCollection)
        {
            sb.AppendLine(outputItem.BaseObject.ToString());
        }
        return sb.ToString();
    }


    private void ClearCommands() => _shell.Commands.Clear();


    private void PrintOutput(string errorMsg, string successMsg)
    {
        if (!string.IsNullOrEmpty(errorMsg))
        {
            Console.Error.WriteLine($"Error!\n {errorMsg}");
        }
        else
        {
            Console.WriteLine(successMsg);
        }
    }
}