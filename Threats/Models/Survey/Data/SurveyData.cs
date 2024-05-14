using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(string titleFormat, NegativesStageData negatives, ObjectsStageData threats)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.threats = threats;
    }

    [JsonProperty("negatives")] private readonly NegativesStageData negatives;
    [JsonProperty("threats")] private readonly ObjectsStageData threats;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStageData NegativesStageData => negatives;
    public IObjectsStageData ObjectsStageData => threats;
}
