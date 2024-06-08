using Newtonsoft.Json;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    public SurveyData(
        string titleFormat,
        NegativesStageData negatives,
        ObjectsStageData objects,
        IntrudersStageData intruders,
        ThreatsStageData threats)
    {
        TitleFormat = titleFormat;

        this.negatives = negatives;
        this.objects = objects;
        this.intruders = intruders;
        this.threats = threats;
    }

    [JsonProperty("negatives")] private readonly NegativesStageData negatives;
    [JsonProperty("objects")] private readonly ObjectsStageData objects;
    [JsonProperty("intruders")] private readonly IntrudersStageData intruders;
    [JsonProperty("threats")] private readonly ThreatsStageData threats;

    [JsonProperty("title")] public string TitleFormat { get; }

    public INegativesStageData NegativesStageData => negatives;
    public IObjectsStageData ObjectsStageData => objects;
    public IIntrudersStageData IntrudersStageData => intruders;
    public IThreatsStageData ThreatsStageData => threats;
}
