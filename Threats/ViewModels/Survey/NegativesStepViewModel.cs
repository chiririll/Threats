using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class NegativesStepViewModel : SurveyStepViewModel<NegativesStep>
{
    private readonly CompositeDisposable questionSub = new();

    private QuestionViewModel question;

    public NegativesStepViewModel(NegativesStep step) : base(step)
    {
        question = new(step.CurrentQuestion);

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
        Question = new(step.CurrentQuestion);
    }

    public override void Dispose()
    {
        base.Dispose();

        questionSub.Dispose();
    }
}
