using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class NegativesStageViewModel : SurveyStageViewModel<NegativesStage>
{
    private readonly CompositeDisposable questionSub = new();

    private QuestionViewModel question;

    public NegativesStageViewModel(NegativesStage stage) : base(stage)
    {
        question = new(stage.CurrentQuestion);

        question.Updated
            .Subscribe(_ => updated.OnNext(default))
            .AddTo(questionSub);
    }

    public QuestionViewModel Question
    {
        get => question;
        private set => this.RaiseAndSetIfChanged(ref question, value);
    }

    public override void Refresh()
    {
        Question = new(stage.CurrentQuestion);
    }

    public override void Dispose()
    {
        base.Dispose();

        questionSub.Dispose();
    }
}
