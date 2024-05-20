using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.Models.Survey.Data;

namespace Threats.ViewModels.Survey;

public class IntrudersStageViewModel : SurveyStageViewModel<IntrudersStage>
{
    private readonly CompositeDisposable questionsSub = new();

    private IntruderViewModel intruder;

    public IntrudersStageViewModel(IntrudersStage stage) : base(stage)
    {
        OptionUpdated = ReactiveCommand.Create(() => updated.OnNext(default));

        intruder = CreateIntruder();
    }

    public IntruderViewModel Intruder
    {
        get => intruder;
        set => this.RaiseAndSetIfChanged(ref intruder, value);
    }

    public IIntrudersStageData Data => stage.Data;

    public ReactiveCommand<Unit, Unit> OptionUpdated { get; }

    public QuestionVM Question1 { get; } = new();
    public QuestionVM Question2 { get; } = new();

    public override void Refresh()
    {
        questionsSub.Clear();
        intruder = CreateIntruder();
    }

    private IntruderViewModel CreateIntruder()
    {
        var current = stage.CurrentIntruder!;
        return new(
            string.Format(stage.Data.NameFormat, current.Title),
            string.Format(stage.Data.PotentialFormat, stage.Data.GetPotentialName(current.Potential)),
            current.Goals);
    }

    public class IntruderViewModel
    {
        public IntruderViewModel(
            string title,
            string potential,
            IEnumerable<string> goals)
        {
            Title = title;
            Potential = potential;
            Goals = new(goals);
        }

        public string Title { get; private set; }
        public string Potential { get; private set; }
        public ObservableCollection<string> Goals { get; private set; }
    }

    public class QuestionVM
    {
        public bool Yes { get; set; }
        public bool No { get; set; }
    }
}
