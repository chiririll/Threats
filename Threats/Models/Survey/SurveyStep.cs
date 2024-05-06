using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public abstract class SurveyStep
{
    protected readonly SurveyState state;

    public SurveyStep(SurveyState state)
    {
        this.state = state;
    }
}
