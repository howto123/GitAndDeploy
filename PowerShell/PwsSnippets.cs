



namespace PwShell;


public class PwsSnippets
{
    public string IsGitRepo { get; init; }
    public string ValidateOriginOrThrow { get; init; }
    public string ValidateBranchOrThrow { get; init; }
    public string GitPush { get; init; }

    public PwsSnippets(string urlOfOrigin, string branch)
    {
        IsGitRepo = "$ignored = git branch";
        ValidateOriginOrThrow = GetValidateOriginOrThrow(urlOfOrigin);
        ValidateBranchOrThrow = GetValidateBranchOrThrow(branch);
        GitPush = "git push";

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
}

