using Newtonsoft.Json;
using Threats.Data;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class NegativesStageData : StageData, INegativesStageData
{
    [JsonProperty("types_question_label")] public string TypesQuestionLabel { get; private set; } = string.Empty;
    [JsonProperty("negatives_question_label")] public string NegativesQuestionLabel { get; private set; } = string.Empty;
}
