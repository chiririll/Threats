using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ObjectsAppendStage : SurveyStage<ObjectsStageState, IObjectsStageData>
{
    public ObjectsAppendStage(
        SurveyState state,
        IObjectsStageData data,
        IEntitiesData entities) : base(state.ObjectsStage, data, entities)
    {
    }

    public IReadOnlyList<Object> Objects => state.Objects;

    public override StageType Type => StageType.Objects;

    public override void Init()
    {
    }

    public bool AddObject(Object obj) => state.AddObject(obj);

    public List<Object> GetAvailableObjects()
    {
        var selected = state.Objects.Select(o => o.Id);
        return entities.Objects.Where(o => !selected.Contains(o.Id)).ToList();
    }

    public override void Save()
    {
    }

    public override bool CanMoveNext() => true;
    public override bool MoveNext() => false;

    public override bool CanMoveBack() => false;
    public override bool MoveBack() => false;
}
