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

    public IReadOnlySet<IntruderBuilder> Intruders => state.SelectedIntruders;

    public override void Save()
    {
        state.BuildIntruders();
    }

    public override bool CanMoveNext() => !state.SelectedIntruders.Any(i => i.Type == Threats.Data.Entities.IntruderType.None);
    public override bool MoveNext() => false;
}
