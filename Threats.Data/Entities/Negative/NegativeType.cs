using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class NegativeType : Entity
{
    public NegativeType(int id, string name) : base(id)
    {
        Name = name;
    }

    [JsonProperty("name")] public string Name { get; }
}
