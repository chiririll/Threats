using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Script : Entity
{
    public Script(int id, string identifier) : base(id)
    {
        Identifier = identifier;
    }

    [JsonProperty("s_id")] public string Identifier { get; }
}
