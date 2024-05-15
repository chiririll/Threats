using System.Collections.ObjectModel;
using System.Linq;
using Threats.Models.Survey;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class ObjectsStageViewModel : SurveyStageViewModel<ObjectsStage>
{
    public ObjectsStageViewModel(ObjectsStage stage) : base(stage)
    {
        PrimaryQuestion = new(stage.Questions[0]);
        Questions = new(stage.Questions.Skip(1).Select(q => new QuestionViewModel(q)));
    }

    public QuestionViewModel PrimaryQuestion { get; }
    public ObservableCollection<QuestionViewModel> Questions { get; }

    public override void Refresh()
    {
    }
}
