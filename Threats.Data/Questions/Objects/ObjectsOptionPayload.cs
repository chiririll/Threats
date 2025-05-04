using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Questions;

public class ObjectsOptionPayload : IOptionPayload
{
    [JsonProperty("add", Order = 11)] private readonly HashSet<int> objectsToAdd;
    [JsonProperty("exclude", Order = 12)] private readonly HashSet<int> objectsToExclude;

    public ObjectsOptionPayload(
        IEnumerable<int> objectsToAdd,
        IEnumerable<int> objectsToExclude)
    {
        this.objectsToAdd = new(objectsToAdd);
        this.objectsToExclude = new(objectsToExclude);
    }

    [JsonIgnore] public IEnumerable<int> ObjectsToAdd => objectsToAdd;
    [JsonIgnore] public IEnumerable<int> ObjectsToExclude => objectsToExclude;
}
