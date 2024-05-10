using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public abstract class SurveyStep<TData> : SurveyStep
    where TData : IStepData
{
    protected readonly TData data;

    public SurveyStep(SurveyState state, TData data) : base(state)
    {
        this.data = data;
    }

    public override string Title => data.Title;
}

public abstract class SurveyStep
{
    protected readonly SurveyState state;

    public SurveyStep(SurveyState state)
    {
        this.state = state;
    }

    public abstract string Title { get; }
}
