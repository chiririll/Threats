using System;
using System.Reactive.Linq;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class NegativeTypesStageViewModel : SurveyStageViewModel<NegativeTypesStage>
{
    public NegativeTypesStageViewModel(NegativeTypesStage stage) : base(stage)
    {
        Question = new(stage.Question!);

        Question.OnUpdate
            .Subscribe(_ => updated.OnNext(default));
    }

    public QuestionViewModel Question { get; }

    public override void Refresh()
    {
    }
}
