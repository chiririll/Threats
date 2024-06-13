using System.Collections.ObjectModel;
using ReactiveUI;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ThreatsStageViewModel : SurveyStageViewModel<ThreatsStage>
{
    private int selectedIndex;

    public ThreatsStageViewModel(ThreatsStage stage) : base(stage)
    {
        Selectors = new(stage.Selectors);
        SelectedThreatIndex = 0;
    }

    public ObservableCollection<ThreatSelector> Selectors { get; }
    public FocusedThreatViewModel? FocusedThreat { get; private set; }

    public int SelectedThreatIndex
    {
        get => selectedIndex;
        set
        {
            if (value < 0 || value >= Selectors.Count)
                return;

            selectedIndex = value;

            FocusedThreat = new(Selectors[selectedIndex], stage.Entities);
            this.RaisePropertyChanged(nameof(FocusedThreat));
        }
    }

    public override void Refresh()
    {
    }
}
