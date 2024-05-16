using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public static class SurveyStageViewModelFactory
{
    public static SurveyStageViewModel Create(SurveyStage stage)
    {
        return stage switch
        {
            NegativesStage negatives => new NegativesStageViewModel(negatives),
            ObjectsStage objects => new ObjectsStageViewModel(objects),
            IntrudersStage intruders => new IntrudersStageViewModel(intruders),

            _ => throw new System.ArgumentException("Invalid stage type"),
        };
    }
}
