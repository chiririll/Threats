using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class ObjectsStageViewModel : SurveyStageViewModel<ObjectsStage>
{
    public ObjectsStageViewModel(ObjectsStage stage) : base(stage)
    {
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
}
