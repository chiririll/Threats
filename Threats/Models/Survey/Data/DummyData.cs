namespace Threats.Models.Survey.Data;

public class DummySurveyData : ISurveyData
{
    public string TitleFormat { get; } = "Step {0}: {1}";

    public INegativesStepData NegativesStepData { get; } = new DummyNegativesData();
    public IThreatsStepData ThreatsStepData { get; } = new DummyThreatsData();

}

public class DummyNegativesData : INegativesStepData
{
    public string Title => "Negatives step";
}

public class DummyThreatsData : IThreatsStepData
{
    public string Title => "Threats step";
}
