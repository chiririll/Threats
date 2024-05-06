using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public static class SurveyStepViewModelFactory
{
    public static SurveyStepViewModel Create(SurveyStep step)
    {
        return step switch
        {
            NegativesStep negatives => new NegativesStepViewModel(negatives),
            ThreatsStep threats => new ThreatsStepViewModel(threats),

            _ => throw new System.ArgumentException("Invalid step type"),
        };
    }
}
