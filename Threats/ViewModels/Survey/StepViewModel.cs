using ReactiveUI;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public sealed class StepViewModel : ViewModelBase
{
    private SurveyStepViewModel? step;

    public StepViewModel()
    {
    }

    public SurveyStepViewModel Step
    {
        get => step!;
        private set => this.RaiseAndSetIfChanged(ref step, value);
    }

    public void SetStep(SurveyStepViewModel step) => Step = step;
    public void SetStep(SurveyStep step) => Step = SurveyStepViewModelFactory.Create(step);
}
