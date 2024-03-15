
using System;
using System.IO;
using System.Management.Automation;
using Exceptions;
using PwShell;

namespace GitAndDeploy;


class GitHandler
{
    private PowerShellHandler? _shell;

    public void GitAndDeploy(Types.Project project, Types.Action action, string activeBranch)
    {
        _shell = new PowerShellHandler();
        var snippets = new PwsSnippets
        (
            project.URLofOrigin,
            activeBranch,
            project.AbsoluteHookDirectory,
            action.Branch
        );

        if (ExecutesWithError(snippets.IsGitRepo)) return;
        if (ExecutesWithError(snippets.IsGitRepo)) return;
        if (ExecutesWithError(snippets.ValidateOriginOrThrow)) return;
        if (ExecutesWithError(snippets.ValidateBranchOrThrow)) return;
        if (ExecutesWithError(snippets.GitPush)) return;
        Console.WriteLine($"git push");
        if (ExecutesWithError(snippets.ChangeToHookDirectory)) return;
        if (ExecutesWithError(snippets.ChangeToActionBranch)) return;
        if (ExecutesWithError(snippets.WriteToLogFile)) return;
        if (ExecutesWithError(snippets.GitAdd)) return;
        if (ExecutesWithError(snippets.GitCommit)) return;
        if (ExecutesWithError(snippets.GitPush)) return;
        Console.WriteLine($"trigger hook");
        Console.WriteLine($"done!");
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