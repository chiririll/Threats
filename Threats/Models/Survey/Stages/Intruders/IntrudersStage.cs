using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Questions;
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

    public LabelWithDescription? Potential { get; private set; }
    public bool? Question1 { get; set; }
    public bool? Question2 { get; set; }

    public override StageType Type => StageType.Intruders;

    public override void Init()
    {
        Potential = GetPotentialString();

        var hasIntruder = CurrentIntruder != null && state.SelectedIntruders.ContainsKey(CurrentIntruder.Id);

        Question1 = hasIntruder ? true : null;
        Question2 = hasIntruder ? true : null;
    }

    public override void Save()
    {
        var selected = !Question1.HasValue || !Question2.HasValue || !Question1.Value || !Question2.Value;

        state.SelectIntruder(CurrentIntruder!, selected);
    }

    public override bool CanMoveNext() => Question1 != null && Question2 != null;
    public override bool CanMoveBack() => currentIndex > 0;

    public override bool MoveNext()
    {
        currentIndex++;
        if (CurrentIntruder == null)
        {
            currentIndex--;
            return false;
        }

        Init();

        return true;
    }

    public override bool MoveBack()
    {
        currentIndex--;
        if (CurrentIntruder == null)
        {
            currentIndex++;
            return false;
        }

        Init();

        return true;
    }

    private LabelWithDescription GetPotentialString()
    {
        var potential = CurrentIntruder!.Potential;
        return new(data.GetPotentialName(potential), data.GetPotentialDescription(potential));
    }
}
