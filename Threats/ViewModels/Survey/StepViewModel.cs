using System;

namespace Threats.ViewModels.Survey;

public sealed class StepViewModel : ViewModelBase
{
    public StepViewModel(SurveyStepViewModel step)
    {
        Step = step;
    }

    public SurveyStepViewModel Step { get; private set; }

    internal void SetStep(SurveyStepViewModel step) => Step = step;
}
