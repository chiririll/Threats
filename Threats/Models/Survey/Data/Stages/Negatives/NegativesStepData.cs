using Newtonsoft.Json;
using Threats.Data;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class NegativesStageData : StageData, INegativesStageData
{
    public NegativesStageData(string title, string typesQuestionLabel, string negativesQuestionLabel) : base(title)
    {
        TypesQuestionLabel = typesQuestionLabel;
        NegativesQuestionLabel = negativesQuestionLabel;
    }

    [JsonProperty("types_question_label")] public string TypesQuestionLabel { get; }
    [JsonProperty("negatives_question_label")] public string NegativesQuestionLabel { get; }
}
