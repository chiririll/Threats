using Newtonsoft.Json;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class ObjectsStageData : StageData, IObjectsStageData
{
    public ObjectsStageData(
        string title,
        string header,
        string objectResultsHeader,
        string addObjectHeader,
        string addObjectButton) : base(title)
    {
        Header = header;
        ObjectResultsHeader = objectResultsHeader;
        AddObjectHeader = addObjectHeader;
        AddObjectButton = addObjectButton;
    }

    [JsonProperty("header")] public string Header { get; }

    [JsonProperty("result_header")] public string ObjectResultsHeader { get; }

    [JsonProperty("add_object_header")] public string AddObjectHeader { get; }
    [JsonProperty("add_object_button")] public string AddObjectButton { get; }
}
