


namespace Types;



#nullable disable
class Action
{
    public string Name { get; set; }
    public string Branch { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Branch: {Branch}";
    }
}