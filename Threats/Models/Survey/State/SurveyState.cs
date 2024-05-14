namespace Threats.Models.Survey.State;

public class SurveyState
{
    public SurveyState()
    {
        NegativesStage = new();
        ThreatsStage = new();
    }

    public NegativesStageState NegativesStage { get; }
    public ThreatsStageState ThreatsStage { get; }

    public SurveyResult GetResult()
    {
        var builder = new SurveyResultBuilder();

        NegativesStage.FillResult(builder);
        ThreatsStage.FillResult(builder);

        return builder.Build();
    }
}
