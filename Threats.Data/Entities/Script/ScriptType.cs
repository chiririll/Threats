using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class ScriptType : Entity
{
    public ScriptType(int id, string name, string task, string? note) : base(id)
    {
        Name = name;
        Task = task;
        Note = note;
    }

    [JsonProperty("name")] public string Name { get; }
    [JsonProperty("task")] public string Task { get; }
    [JsonProperty("note")] public string? Note { get; }

    public override string ToString() => $"Т{Id}";
}
