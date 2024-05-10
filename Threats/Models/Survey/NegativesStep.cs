using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativesStep : SurveyStep<INegativesStepData>
{
    public NegativesStep(SurveyState state, INegativesStepData data) : base(state, data)
    {
    }
}
