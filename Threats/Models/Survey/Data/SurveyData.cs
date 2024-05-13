using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(string titleFormat, NegativesStepData negatives, ThreatsStepData threats)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.threats = threats;
    }

    [JsonProperty("negatives")] private readonly NegativesStepData negatives;
    [JsonProperty("threats")] private readonly ThreatsStepData threats;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStepData NegativesStepData => negatives;
    public IThreatsStepData ThreatsStepData => threats;
}
