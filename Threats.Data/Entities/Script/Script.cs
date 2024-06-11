using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Script
{
    [JsonProperty("examples")] private readonly List<string> examples = new();

    public Script(ScriptId id)
    {
    }

    [JsonProperty("id")] public ScriptId Id { get; }
    [JsonProperty("desc")] public string Description { get; }
    [JsonProperty("note")] public string? Note { get; }

    public IReadOnlyList<string> Examples => examples;
}
