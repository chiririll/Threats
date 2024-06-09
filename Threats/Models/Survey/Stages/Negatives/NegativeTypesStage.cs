using System.Linq;
using Threats.Data;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativeTypesStage : SurveyStage<NegativesStageState, INegativesStageData>
{
    public NegativeTypesStage(
        SurveyState state,
        INegativesStageData data,
        IEntitiesData entities) : base(state.NegativesStage, data, entities)
    {
    }

    public Question? Question { get; private set; }

    public override StageType Type => StageType.Negatives;
    public override void Init()
    {
        Question = new(string.Empty, entities.NegativeTypes.Select(
            t => new Option(t.Id, t.Name, null, t.Description)
            {
                Selected = state.Types.Contains(t)
            }));
    }

    public override void Save()
    {
        var selectedTypes = Question!.Selected.Select(t => t.Id);
        state.SetTypes(entities.NegativeTypes.Where(t => selectedTypes.Contains(t.Id)));
    }

    public override bool CanMoveNext() => Question!.Selected.Count > 0;
    public override bool MoveNext() => false;

    public override bool CanMoveBack() => false;
    public override bool MoveBack() => false;
}
