using Newtonsoft.Json;
using Threats.Data;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class NegativesStepData : StepData, INegativesStepData
{
    public NegativesStepData(string title, string typesQuestionLabel, string negativesQuestionLabel) : base(title)
    {
        TypesQuestionLabel = typesQuestionLabel;
        NegativesQuestionLabel = negativesQuestionLabel;
    }

    [JsonProperty("types_question_label")] public string TypesQuestionLabel { get; }
    [JsonProperty("negatives_question_label")] public string NegativesQuestionLabel { get; }
}
