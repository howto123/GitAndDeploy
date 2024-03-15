



using System;

namespace PwShell;


public class PwsSnippets
{
    public string IsGitRepo { get; init; }
    public string ValidateOriginOrThrow { get; init; }
    public string ValidateBranchOrThrow { get; init; }
    public string GitPush { get; init; }
    public string ChangeToHookDirectory { get; init; }
    public string ChangeToActionBranch { get; init; }
    public string WriteToLogFile { get; init; }
    public string GitAdd { get; init; }
    public string GitCommit { get; init; }

    public PwsSnippets(string urlOfOrigin, string branch, string absoluteHookDirectoryPath, string actionBranch)
    {
        IsGitRepo = "$ignored = git branch";
        ValidateOriginOrThrow = GetValidateOriginOrThrow(urlOfOrigin);
        ValidateBranchOrThrow = GetValidateBranchOrThrow(branch);
        GitPush = "$ignored = git push";
        ChangeToHookDirectory = $"cd {absoluteHookDirectoryPath}";
        ChangeToActionBranch = GetChangeToActoinBranch(actionBranch);
        WriteToLogFile = $"echo {DateTime.Now} > logfile.txt";
        GitAdd = "$ignored = git add .";
        GitCommit = "$ignored = git commit -m GitAndDeploy";
    }


    private static string _curlyOpen = "{";
    private static string _curlyClose = "}";

    private static string GetValidateOriginOrThrow(string urlOfOrigin)
    {
        return $@"$url = git config --get remote.origin.url; if($url -ne '{urlOfOrigin}'){_curlyOpen}Write-Error -Message 'Bad origin'{_curlyClose}";
    }
    private static string GetValidateBranchOrThrow(string branch)
    {
        return $@"$b=git branch; if(!($b -Match '\*\s{branch}')){_curlyOpen}Write-Error -Message 'Bad branch'{_curlyClose}";
    }

    private static string GetChangeToActoinBranch(string actionBranch)
    {
        //var insideCatch = $"if(!($_.Exeption.Message -Match 'Already on')){_curlyOpen}Write-Error -Message $_.Exeption.Message{_curlyClose}";
        var insideCatch = $"echo $_.Exeption.Message";
        //return $"try{_curlyOpen}git checkout {actionBranch}{_curlyClose} catch {_curlyOpen + insideCatch + _curlyClose}";
        //return $"try{_curlyOpen}echo hi; git checkout test; echo hiagain;{actionBranch}{_curlyClose} catch {_curlyOpen + "echo insideCatch;" + insideCatch + _curlyClose}";
        return $@"$b=git branch; if(!($b -Match '\*\s{actionBranch}')){_curlyOpen}git checkout {actionBranch}{_curlyClose}";
    }
}

