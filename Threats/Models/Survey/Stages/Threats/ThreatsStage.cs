using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStage : SurveyStage<ThreatsStageState, IThreatsStageData>
{
    public ThreatsStage(SurveyState state, IThreatsStageData data, IEntitiesData entities) : base(state.ThreatsStage, data, entities)
    {
    }

    public override void Save()
    {
    }

    public override bool CanMoveNext() => true;
    public override bool MoveNext() => false;
}
