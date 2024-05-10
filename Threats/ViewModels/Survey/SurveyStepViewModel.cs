using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public abstract class SurveyStepViewModel<TStep> : SurveyStepViewModel
    where TStep : SurveyStep
{
    protected readonly TStep step;

    public SurveyStepViewModel(TStep step)
    {
        this.step = step;
    }
}

public abstract class SurveyStepViewModel : ViewModelBase
{
}
