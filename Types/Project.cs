

using System.Collections.Generic;

namespace Types;



#nullable disable
class Project
{
    public string Name { get; set; }
    public string AbsoluteHookDirectory { get; set; }
    public List<Types.Action> Actions { get; set; }

    public override string ToString()
    {
        var actionsString = "";
        Actions.ForEach(a => actionsString += a.ToString() + "\n");
        return $"Name: {Name}, AbsoluteHookDirectory: {AbsoluteHookDirectory}: Actions:\n{actionsString}";
    }
}