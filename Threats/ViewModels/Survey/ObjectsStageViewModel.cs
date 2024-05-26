using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using Threats.Models.Questions;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class ObjectsStageViewModel : SurveyStageViewModel<ObjectsStage>
{
    private readonly CompositeDisposable questionsSub = new();

    public ObjectsStageViewModel(ObjectsStage stage) : base(stage)
    {
        PrimaryQuestion = new(stage.Questions[0]);

        Questions = new(stage.Questions.Skip(1).Select(
            (q, i) =>
            {
                var vm = new YesNoOptionContainer<Question>(
                i, q,
                (q, yes, no) =>
                {
                    q.Options.ElementAt(0).Selected = yes;
                    q.Options.ElementAt(1).Selected = no;
                },
                new LabelWithHelpButtonViewModel(q.Label));

                vm.OptionUpdated
                    .Subscribe(OnQuestionUpdated)
                    .AddTo(questionsSub);

                return vm;
            }));

        PrimaryQuestion.OnUpdate
            .Subscribe(OnQuestionUpdated)
            .AddTo(questionsSub);
    }

    public QuestionViewModel PrimaryQuestion { get; }
    public ObservableCollection<YesNoOptionContainer<Question>> Questions { get; }

    public string Header => stage.Data.Header;

    private void OnQuestionUpdated(Unit _)
    {
        updated.OnNext(default);
    }

    public override void Refresh()
    {
    }
}
