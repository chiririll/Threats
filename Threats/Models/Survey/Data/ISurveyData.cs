namespace Threats.Models.Survey.Data;

public interface ISurveyData
{
    public string TitleFormat { get; }

    public INegativesStepData NegativesStepData { get; }
    public IThreatsStepData ThreatsStepData { get; }
}
