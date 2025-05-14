using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Entities;

namespace Threats.Data;

public class EntitiesData : IEntitiesData
{
    [JsonProperty("threats")] private readonly List<Threat> threats = new();
    [JsonProperty("objects")] private readonly List<Object> objects = new();

    [JsonProperty("script_types")] private readonly List<ScriptType> scriptTypes = new();
    [JsonProperty("scripts")] private readonly List<Script> scripts = new();

    [JsonProperty("negative_types")] private readonly List<NegativeType> negativeTypes = new();
    [JsonProperty("negatives")] private readonly List<Negative> negatives = new();

    [JsonProperty("intruders")] private readonly List<IntruderData> intruders = new();

    public EntitiesData(
        IEnumerable<Threat> threats,
        IEnumerable<Object> objects,
        IEnumerable<ScriptType> scriptTypes,
        IEnumerable<Script> scripts,
        IEnumerable<NegativeType> negativeTypes,
        IEnumerable<Negative> negatives,
        IEnumerable<IntruderData> intruders)
    {
        this.threats.AddRange(threats);
        this.objects.AddRange(objects);

        this.scriptTypes.AddRange(scriptTypes);
        this.scripts.AddRange(scripts);

        this.negativeTypes.AddRange(negativeTypes);
        this.negatives.AddRange(negatives);

        this.intruders.AddRange(intruders);
    }

    [JsonIgnore] public IReadOnlyList<Threat> Threats => threats;
    [JsonIgnore] public IReadOnlyList<Object> Objects => objects;

    [JsonIgnore] public IReadOnlyList<ScriptType> ScriptTypes => scriptTypes;
    [JsonIgnore] public IReadOnlyList<Script> Scripts => scripts;

    [JsonIgnore] public IReadOnlyList<NegativeType> NegativeTypes => negativeTypes;
    [JsonIgnore] public IReadOnlyList<Negative> Negatives => negatives;

    [JsonIgnore] public IReadOnlyList<IntruderData> Intruders => intruders;

    public static EntitiesData FromJson(string json) => JsonConvert.DeserializeObject<EntitiesData>(json)!;
    public string ToJson() => JsonConvert.SerializeObject(this, Formatting.None);

    public Object? GetObjectById(int id) => objects.Find(o => o.Id.Equals(id));

    public ScriptType? GetScriptTypeById(int id) => scriptTypes.Find(t => t.Id.Equals(id));
    public Script? GetScriptById(ScriptId id) => scripts.Find(s => s.Id.Equals(id));
}
