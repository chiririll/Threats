using System;
using System.Reactive.Linq;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class NegativesStageViewModel : SurveyStageViewModel<NegativesStage>
{
    public NegativesStageViewModel(NegativesStage stage) : base(stage)
    {
        Question = new(stage.Question!);

        Question.Updated
            .Subscribe(_ => updated.OnNext(default));
    }

    public QuestionViewModel Question { get; }

    public override void Refresh()
    {
    }
}
