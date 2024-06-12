using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Script
{
    [JsonProperty("examples")] private readonly List<string> examples;

    public Script(ScriptId id, string description, string? note, IEnumerable<string> examples)
    {
        Id = id;
        Description = description;
        Note = note;

        this.examples = new(examples);
    }

    [JsonProperty("id", Order = -100)] public ScriptId Id { get; }
    [JsonProperty("desc")] public string Description { get; }
    [JsonProperty("note")] public string? Note { get; }

    [JsonIgnore] public IReadOnlyList<string> Examples => examples;
}
