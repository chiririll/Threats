using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ObjectsStage : SurveyStage<ObjectsStageState, IObjectsStageData>
{
    public ObjectsStage(SurveyState state, IObjectsStageData data, IEntitiesData entities) : base(state.ObjectsStage, data, entities)
    {
    }

    public override void Save()
    {
    }

    public override bool CanMoveNext() => true;
    public override bool MoveNext() => false;
}
