using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class NegativeType : Entity
{
    public NegativeType(int id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    [JsonProperty("name")] public string Name { get; }
    [JsonProperty("description")] public string Description { get; }
}
