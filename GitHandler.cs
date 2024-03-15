
using System;
using System.IO;
using System.Management.Automation;
using Exceptions;
using PwShell;

namespace GitAndDeploy;




class GitHandler
{
    private PowerShellHandler? _shell;

    public void GitAndDeploy(Types.Project project, Types.Action action, string comment)
    {
        _shell = new PowerShellHandler();
        var snippets = new PwsSnippets
        (
            "https://github.com/howto123/GitAndDeploy.git",
            //Environment.GetEnvironmentVariable("ORIGIN")!,
            "main",
            Environment.GetEnvironmentVariable("HOOKPATH")!,
            "test"
        );

        if (ExecutesWithError("dir")) return;
        if (ExecutesWithError(snippets.IsGitRepo)) return;
        if (ExecutesWithError(snippets.IsGitRepo)) return;
        if (ExecutesWithError(snippets.ValidateOriginOrThrow)) return;
        if (ExecutesWithError(snippets.ValidateBranchOrThrow)) return;
        if (ExecutesWithError(snippets.GitPush)) return;
        Console.WriteLine($"after first push");
        if (ExecutesWithError(snippets.ChangeToHookDirectory)) return;
        if (ExecutesWithError(snippets.ChangeToActionBranch)) return;
        if (ExecutesWithError(snippets.WriteToLogFile)) return;
        if (ExecutesWithError(snippets.GitAdd)) return;
        if (ExecutesWithError(snippets.GitCommit)) return;
        if (ExecutesWithError(snippets.GitPush)) return;
        Console.WriteLine($"after first push");
        if (ExecutesWithError("echo done!")) return;

    }

    private bool ExecutesWithError(string shellCode)
    {
        if(_shell is null) throw new PreconditionException("_shell was not initialized!");
        try
        {
            _shell.Execute(shellCode);
        }
        catch( PowerShellException)
        {
            // Message was already printed in Powershellhandler
            return true;
        }
        return false;
    }
}