using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class ObjectsStageViewModel : SurveyStageViewModel<ObjectsStage>
{
    private readonly CompositeDisposable questionsSub = new();

    public ObjectsStageViewModel(ObjectsStage stage) : base(stage)
    {
        PrimaryQuestion = new(stage.Questions[0]);
        Questions = new(stage.Questions.Skip(1).Select(q => new QuestionViewModel(q)));

        PrimaryQuestion.Updated
            .Subscribe(OnQuestionUpdated)
            .AddTo(questionsSub);

        foreach (var question in Questions)
        {
            question.Updated
                .Subscribe(OnQuestionUpdated)
                .AddTo(questionsSub);
        }
    }

    public QuestionViewModel PrimaryQuestion { get; }
    public ObservableCollection<QuestionViewModel> Questions { get; }

    private void OnQuestionUpdated(Unit _)
    {
        updated.OnNext(default);
    }

    public override void Refresh()
    {
    }
}
