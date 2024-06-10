using System.Collections.ObjectModel;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ThreatsStageViewModel : SurveyStageViewModel<ThreatsStage>
{
    public ThreatsStageViewModel(ThreatsStage stage) : base(stage)
    {
        Selectors = new(stage.Selectors);
    }

    public ObservableCollection<ThreatSelector> Selectors { get; }

    public override void Refresh()
    {
    }
}
