namespace Threats.Models.Survey.Data;

public interface ISurveyData
{
    public string TitleFormat { get; }

    public INegativesStageData NegativesStageData { get; }
    public IThreatsStageData ThreatsStageData { get; }
}
