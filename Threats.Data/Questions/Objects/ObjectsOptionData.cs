using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Questions;

public class ObjectsOptionData : OptionData, IOptionData
{
    [JsonProperty("add", Order = 11)] private readonly HashSet<int> objectsToAdd;
    [JsonProperty("exclude", Order = 12)] private readonly HashSet<int> objectsToExclude;

    public ObjectsOptionData(
        int id,
        string? group,
        string label,
        string? helpText,
        IEnumerable<int> objectsToAdd,
        IEnumerable<int> objectsToExclude) : base(id, group, label, helpText)
    {
        this.objectsToAdd = new(objectsToAdd);
        this.objectsToExclude = new(objectsToExclude);
    }

    [JsonIgnore] public IReadOnlySet<int> ObjectsToAdd => objectsToAdd;
    [JsonIgnore] public IReadOnlySet<int> ObjectsToExclude => objectsToExclude;
}
