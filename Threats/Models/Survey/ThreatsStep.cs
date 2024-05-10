using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStep : SurveyStep<IThreatsStepData>
{
    public ThreatsStep(SurveyState state, IThreatsStepData data) : base(state, data)
    {
    }
}
