using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using ReactiveUI;
using Threats.Models.Questions;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class IntrudersStageViewModel : SurveyStageViewModel<IntrudersStage>
{
    private readonly CompositeDisposable questionsSub = new();

    private IntruderViewModel intruder;

    public IntrudersStageViewModel(IntrudersStage stage) : base(stage)
    {
        intruder = CreateIntruder();

        intruder.TypeQuestion.Updated
            .Subscribe(_ => updated.OnNext(default))
            .AddTo(questionsSub);

        intruder.IncludedQuestion.Updated
            .Subscribe(_ => updated.OnNext(default))
            .AddTo(questionsSub);
    }

    public IntruderViewModel Intruder
    {
        get => intruder;
        set => this.RaiseAndSetIfChanged(ref intruder, value);
    }

    public override void Refresh()
    {
        questionsSub.Clear();
        intruder = CreateIntruder();

        intruder.TypeQuestion.Updated
            .Subscribe(_ => updated.OnNext(default))
            .AddTo(questionsSub);

        intruder.IncludedQuestion.Updated
            .Subscribe(_ => updated.OnNext(default))
            .AddTo(questionsSub);
    }

    private IntruderViewModel CreateIntruder()
    {
        var current = stage.CurrentIntruder!;
        return new(
            string.Format(stage.Data.NameFormat, current.Title),
            string.Format(stage.Data.PotentialFormat, stage.Data.GetPotentialName(current.Potential)),
            current.Goals,
            stage.IntruderIncluded,
            stage.IntruderType);
    }

    public class IntruderViewModel
    {
        public IntruderViewModel(
            string title,
            string potential,
            IEnumerable<string> goals,
            Question includedQuestion,
            Question typeQuestion)
        {
            Title = title;
            Potential = potential;
            Goals = new(goals);

            IncludedQuestion = new(includedQuestion);
            TypeQuestion = new(typeQuestion);
        }

        public string Title { get; private set; }
        public string Potential { get; private set; }
        public ObservableCollection<string> Goals { get; private set; }

        public QuestionViewModel IncludedQuestion { get; private set; }
        public QuestionViewModel TypeQuestion { get; private set; }
    }
}
