namespace Threats.Models.Survey.State;

public class SurveyState
{
    public SurveyState()
    {
        NegativesStep = new();
        ThreatsStep = new();
    }

    public NegativesStepState NegativesStep { get; }
    public ThreatsStepState ThreatsStep { get; }

    public SurveyResult GetResult()
    {
        var builder = new SurveyResultBuilder();

        NegativesStep.FillResult(builder);
        ThreatsStep.FillResult(builder);

        return builder.Build();
    }
}
