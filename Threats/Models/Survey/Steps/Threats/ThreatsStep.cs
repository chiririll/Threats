using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStep : SurveyStep<ThreatsStepState, IThreatsStepData>
{
    public ThreatsStep(SurveyState state, IThreatsStepData data) : base(state.ThreatsStep, data)
    {
    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }

    public override bool CanMoveNext() => true;
    public override bool MoveNext() => false;
}
