using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public static class SurveyStageViewModelFactory
{
    public static SurveyStageViewModel Create(SurveyManager manager, SurveyStage stage)
    {
        return stage switch
        {
            NegativesStage negatives => new NegativesStageViewModel(negatives),
            NegativeTypesStage negatives => new NegativeTypesStageViewModel(negatives),
            ObjectsStage objects => new ObjectsStageViewModel(manager, objects),
            ObjectsAppendStage objectsAppend => new ObjectsAppendStageViewModel(objectsAppend),
            IntrudersStage intruders => new IntrudersStageViewModel(intruders),
            IntrudersTypeStage intrudersType => new IntrudersTypeStageViewModel(intrudersType),
            ThreatsStage threats => new ThreatsStageViewModel(threats),

            _ => throw new System.ArgumentException("Invalid stage type"),
        };
    }
}
