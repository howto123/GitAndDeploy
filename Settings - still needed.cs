


namespace GitAndDeploy;


#nullable disable
class Settings 
{
    public string GitHookPath { get; set; }
    public string HookBranch { get; set; }
    public string HookCommitMessage { get; set; }
    public string LogFile { get; set; }

    public override string ToString()
    {
        return @$"
The Settings are
  GitHookPath: {GitHookPath}
  HookBranch: {HookBranch}
  HookCommitMessage: {HookCommitMessage}
  LogFile: {LogFile}
";
    }
}