using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Threat : Entity
{
    public Threat(
        int id,
        string name,
        string description,
        SafetyViolations violations,
        IReadOnlyList<Intruder> intruders,
        IReadOnlyList<int> objectIds,
        IReadOnlyList<ScriptId> scriptIds
#if THREATS_DATE
        , System.DateOnly addDate,
        System.DateOnly updateDate
#endif
        ) : base(id)
    {
        Name = name;
        Description = description;
        Violations = violations;
        Intruders = intruders;
        ObjectIds = objectIds;
        ScriptIds = scriptIds;
#if THREATS_DATE
        AddDate = addDate;
        UpdateDate = updateDate;
#endif
    }

    [JsonProperty("name")] public string Name { get; }
    [JsonProperty("desc")] public string Description { get; }
    [JsonProperty("violations")] public SafetyViolations Violations { get; }

    [JsonProperty("intruders")] public IReadOnlyList<Intruder> Intruders { get; }
    [JsonProperty("objects")] public IReadOnlyList<int> ObjectIds { get; }
    [JsonProperty("scripts")] public IReadOnlyList<ScriptId> ScriptIds { get; }

#if THREATS_DATE
    [JsonProperty("added")] public DateOnly AddDate { get; }
    [JsonProperty("updated")] public DateOnly UpdateDate { get; }
#endif
}
