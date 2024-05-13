using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStep : SurveyStep<ThreatsStepState, IThreatsStepData>
{
    public ThreatsStep(SurveyState state, IThreatsStepData data, IEntitiesData entities) : base(state.ThreatsStep, data, entities)
    {
    }

    public override void Save()
    {
    }

    public override bool CanMoveNext() => true;
    public override bool MoveNext() => false;
}
