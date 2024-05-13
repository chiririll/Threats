using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Negative : Entity
{
    public Negative(int id, string name, int typeId) : base(id)
    {
        Name = name;
        TypeId = typeId;
    }

    [JsonProperty("name")] public string Name { get; }
    [JsonProperty("type")] public int TypeId { get; }
}
