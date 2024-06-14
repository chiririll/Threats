using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class NegativesStageViewModel : SurveyStageViewModel<NegativesStage>
{
    public NegativesStageViewModel(NegativesStage stage, bool isBold = false) : base(stage)
    {
        var questions = stage.Questions.Select(q => new QuestionViewModel(q, true));

        Questions = new(questions);

        foreach (var question in Questions)
        {
            question.OnUpdate.Subscribe(_ => updated.OnNext(default));
        }
    }

    public ObservableCollection<QuestionViewModel> Questions { get; }

    public override void Refresh()
    {
    }
}
