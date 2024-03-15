
using System;
using System.IO;
using System.Management.Automation;
using PwShell;

namespace GitAndDeploy;




class GitHandler
{
    private PowerShell _shell = PowerShell.Create();

    private string _currentPath;
    private string _commitBranch;
    private readonly string _absoluteHookRepoPath;
    private readonly string _logfile;
    private readonly string _hookBranch;
    private readonly string _hookMessage;

    public GitHandler(string currentPath, string commitBranch, Settings settings)
    {
        _currentPath = currentPath;
        _commitBranch = commitBranch;
        _absoluteHookRepoPath = settings.GitHookPath;
        _logfile = settings.LogFile;
        _hookBranch = settings.HookBranch;
        _hookMessage = settings.HookCommitMessage;
    }

    public static void GitAndDeploy(Types.Project project, Types.Action action, string comment)
    {
        //Console.WriteLine(Environment.GetEnvironmentVariable("ORIGIN"));
        
        var shell = new PowerShellHandler();
        var snippets = new PwsSnippets
        (
            "https://github.com/howto123/GitAndDeploy.git",
            //Environment.GetEnvironmentVariable("ORIGIN")!,
            "main"
        );


        //shell.Execute(snippets.IsGitRepo);

        Console.WriteLine($"Snippet is:");     
        Console.WriteLine($"{snippets.ValidateOriginOrThrow}");
        shell.Execute(snippets.ValidateOriginOrThrow);
        
        shell.Execute(snippets.ValidateBranchOrThrow);

        Console.WriteLine($"{snippets.GitPush}");
        shell.Execute(snippets.GitPush);
        // shell.Execute(snippets.ChangeToHookDirectory);
        // shell.Execute(snippets.ChangeToActionBranch);
        // shell.Execute(snippets.WriteToLogFile);
        // shell.Execute(snippets.GitAdd);
        // shell.Execute(snippets.GitCommit);
        // shell.Execute(snippets.GitPush);
        

    }

    public void GitAndDeploy(string message)
    {
        GitSetBranch(_commitBranch);
        GitAddAll();
        GitCommit(message);
        GitPush();

        ChangeDirectoryTo(_absoluteHookRepoPath);
        GitSetBranch(_hookBranch);
        UpdateLogFile();
        GitAddAll();
        GitCommit(_hookMessage);
        GitPush();
        ChangeDirectoryTo(_currentPath);
    }

    private void GitSetBranch(string branch)
    {
        _shell.AddScript($"git checkout {branch}");
        _shell.Invoke();
    }

    private void GitAddAll()
    {
        _shell.AddScript($"git add .");
        _shell.Invoke();
    }

    private void GitCommit(string message)
    {
        _shell.AddScript($"git commit -m {message}");
        _shell.Invoke();
    }

    private void GitPush()
    {
        _shell.AddScript($"git push");
        _shell.Invoke();
    }

    private void ChangeDirectoryTo(string absolutePath)
    {
        _shell.AddScript($"cd {absolutePath}");
        _shell.Invoke();
    }

    private void UpdateLogFile()
    {
        _shell.AddScript($"echo {DateTime.Now} > {_logfile}");
        _shell.Invoke();
    }
}