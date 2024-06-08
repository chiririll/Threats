using Threats.Data;

namespace Threats.Models.Survey.State;

public class SurveyState
{
    public SurveyState() : this(new(), new(), new(), new())
    {
    }

    public SurveyState(
        NegativesStageState negatives,
        ObjectsStageState objects,
        IntrudersStageState intruders,
        ThreatsStageState threats)
    {
        NegativesStage = negatives;
        ObjectsStage = objects;
        IntrudersStage = intruders;
        ThreatsStage = threats;
    }

    public NegativesStageState NegativesStage { get; }
    public ObjectsStageState ObjectsStage { get; }
    public IntrudersStageState IntrudersStage { get; }
    public ThreatsStageState ThreatsStage { get; }

    public SurveyResult GetResult()
    {
        var builder = new SurveyResultBuilder();

        NegativesStage.FillResult(builder);
        ObjectsStage.FillResult(builder);
        IntrudersStage.FillResult(builder);
        ThreatsStage.FillResult(builder);

        return builder.Build();
    }
}
