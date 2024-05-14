using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ThreatsStageViewModel : SurveyStageViewModel<ThreatsStage>
{
    public ThreatsStageViewModel(ThreatsStage stage) : base(stage)
    {
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
}
