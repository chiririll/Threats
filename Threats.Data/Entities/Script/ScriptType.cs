namespace Threats.Data.Entities;

public class ScriptType : Entity
{
    public ScriptType(int id) : base(id)
    {
    }

    public string Name { get; }
    public string Task { get; }
    public string? Note { get; }
}
