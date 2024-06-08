namespace Threats.Models.Survey.Data;

public interface ISurveyData
{
    public string TitleFormat { get; }

    public INegativesStageData NegativesStageData { get; }
    public IObjectsStageData ObjectsStageData { get; }
    public IIntrudersStageData IntrudersStageData { get; }
    public IThreatsStageData ThreatsStageData { get; }
}
