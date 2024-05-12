using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Entities;

namespace Threats.Data;

public class EntitiesData
{
    [JsonProperty("threats")] private readonly List<Threat> threats = new();
    [JsonProperty("objects")] private readonly List<Object> objects = new();

    [JsonProperty("negative_types")] private readonly List<NegativeType> negativeTypes = new();
    [JsonProperty("negatives")] private readonly List<Negative> negatives = new();

    public EntitiesData(
        IEnumerable<Threat> threats,
        IEnumerable<Object> objects,
        IEnumerable<NegativeType> negativeTypes,
        IEnumerable<Negative> negatives)
    {
        this.threats.AddRange(threats);
        this.objects.AddRange(objects);

        this.negativeTypes.AddRange(negativeTypes);
        this.negatives.AddRange(negatives);
    }

    [JsonIgnore] public IReadOnlyList<Threat> Threats => threats;
    [JsonIgnore] public IReadOnlyList<Object> Objects => objects;

    [JsonIgnore] public IReadOnlyList<NegativeType> NegativeTypes => negativeTypes;
    [JsonIgnore] public IReadOnlyList<Negative> Negatives => negatives;

    public static EntitiesData FromJson(string json)
    {
        return JsonConvert.DeserializeObject<EntitiesData>(json)!;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
