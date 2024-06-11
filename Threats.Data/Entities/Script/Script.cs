using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Script : Entity
{
    [JsonProperty("examples")] private readonly List<string> examples = new();

    public Script(int type, int id) : base(id)
    {
        Type = type;
    }

    [JsonProperty("t")] public int Type { get; }
    [JsonProperty("desc")] public string Description { get; }
    [JsonProperty("note")] public string? Note { get; }

    public IReadOnlyList<string> Examples => examples;
}
