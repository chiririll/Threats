using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.Stages.Intruders;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class IntrudersTypeStage : SurveyStage<IntrudersStageState, IIntrudersStageData>
{
    public IntrudersTypeStage(
        SurveyState state,
        IIntrudersStageData data,
        IEntitiesData entities) : base(state.IntrudersStage, data, entities)
    {
    }

    public IEnumerable<IntruderBuilder> Intruders => state.SelectedIntruders.Values;

    public override StageType Type => StageType.Intruders;

    public override void Init()
    {
    }

    public override void Save()
    {
    }

    public override bool CanMoveNext() => !state.SelectedIntruders.Values.Any(i => i.Type.Equals(Threats.Data.Entities.IntruderType.None));
    public override bool MoveNext() => false;

    public override bool CanMoveBack() => false;
    public override bool MoveBack() => false;
}
