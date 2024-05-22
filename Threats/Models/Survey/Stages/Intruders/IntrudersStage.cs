using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class IntrudersStage : SurveyStage<IntrudersStageState, IIntrudersStageData>
{
    private int currentIndex = 0;

    public IntrudersStage(
        SurveyState state,
        IIntrudersStageData data,
        IEntitiesData entities) : base(state.IntrudersStage, data, entities)
    {
    }

    public IntruderData? CurrentIntruder => currentIndex >= 0 && currentIndex < entities.Intruders.Count
        ? entities.Intruders[currentIndex]
        : null;

    public bool? Question1 { get; set; }
    public bool? Question2 { get; set; }

    public override void Save()
    {
        if (!Question1.HasValue || !Question2.HasValue || !Question1.Value || !Question2.Value)
        {
            return;
        }

        state.SelectIntruder(CurrentIntruder!.Id);
    }

    public override bool CanMoveNext() => Question1 != null && Question2 != null;

    public override bool MoveNext()
    {
        currentIndex++;
        if (CurrentIntruder == null)
        {
            return false;
        }

        Question1 = null;
        Question2 = null;

        return true;
    }
}
