using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(string titleFormat, NegativesStageData negatives, ObjectsStageData objects)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.objects = objects;
    }

    [JsonProperty("negatives")] private readonly NegativesStageData negatives;
    [JsonProperty("objects")] private readonly ObjectsStageData objects;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStageData NegativesStageData => negatives;
    public IObjectsStageData ObjectsStageData => objects;
}
