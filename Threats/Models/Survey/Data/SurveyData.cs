using Threats.Data;

namespace Threats.Models.Survey.Data;

public class SurveyData : ISurveyData
{
    private readonly NegativesStepData negatives;
    private readonly ThreatsStepData threats;

    public SurveyData(EntitiesData data)
    {
        negatives = new(data);
        threats = new();
    }

    public string TitleFormat => "{0}: {1}";

    public INegativesStepData NegativesStepData => negatives;
    public IThreatsStepData ThreatsStepData => threats;
}
