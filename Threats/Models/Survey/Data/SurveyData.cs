using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(string titleFormat, NegativesStageData negatives, ThreatsStageData threats)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.threats = threats;
    }

    [JsonProperty("negatives")] private readonly NegativesStageData negatives;
    [JsonProperty("threats")] private readonly ThreatsStageData threats;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStageData NegativesStageData => negatives;
    public IThreatsStageData ThreatsStageData => threats;
}
