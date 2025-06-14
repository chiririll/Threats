using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Parser.IntrudersList.Data;

[JsonObject(MemberSerialization.OptIn)]
public class IntruderTypeContainer
{
    [JsonProperty("id")] public int IntruderId { get; private set; }
    [JsonProperty("negatives")] public List<int> NegativeIds { get; private set; } = new();
}
