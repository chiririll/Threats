using Newtonsoft.Json;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class ObjectsStageData : StageData, IObjectsStageData
{
    [JsonProperty("header")] public string Header { get; private set; } = string.Empty;

    [JsonProperty("result_header")] public string ObjectResultsHeader { get; private set; } = string.Empty;

    [JsonProperty("add_object_header")] public string AddObjectHeader { get; private set; } = string.Empty;
    [JsonProperty("add_object_button")] public string AddObjectButton { get; private set; } = string.Empty;
}
