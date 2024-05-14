using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Questions;

public class ObjectsOptionData : OptionData
{
    [JsonProperty("add")] private readonly HashSet<int> objectsToAdd;

    public ObjectsOptionData(
        int id,
        string? group,
        string label,
        string? helpText,
        IEnumerable<int> objectsToAdd) : base(id, group, label, helpText)
    {
        this.objectsToAdd = new(objectsToAdd);
    }

    [JsonIgnore] public IReadOnlySet<int> ObjectsToAdd => objectsToAdd;
}
