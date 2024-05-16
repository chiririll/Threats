namespace Threats.Models.Survey.State;

public class SurveyState
{
    public SurveyState()
    {
        NegativesStage = new();
        ObjectsStage = new();
        IntrudersStage = new();
    }

    public NegativesStageState NegativesStage { get; }
    public ObjectsStageState ObjectsStage { get; }
    public IntrudersStageState IntrudersStage { get; }

    public SurveyResult GetResult()
    {
        var builder = new SurveyResultBuilder();

        NegativesStage.FillResult(builder);
        ObjectsStage.FillResult(builder);
        IntrudersStage.FillResult(builder);

        return builder.Build();
    }
}
