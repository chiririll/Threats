using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(
        string titleFormat,
        NegativesStageData negatives,
        ObjectsStageData objects,
        IntrudersStageData intruders)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.objects = objects;
        this.intruders = intruders;
    }

    [JsonProperty("negatives")] private readonly NegativesStageData negatives;
    [JsonProperty("objects")] private readonly ObjectsStageData objects;
    [JsonProperty("intruders")] private readonly IntrudersStageData intruders;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStageData NegativesStageData => negatives;
    public IObjectsStageData ObjectsStageData => objects;
    public IIntrudersStageData IntrudersStageData => intruders;
}
