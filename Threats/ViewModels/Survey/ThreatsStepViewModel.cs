using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ThreatsStepViewModel : SurveyStepViewModel<ThreatsStep>
{
    public ThreatsStepViewModel(ThreatsStep step) : base(step)
    {
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
}
