using System.Linq;
using Threats.Data;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativesStage : SurveyStage<NegativesStageState, INegativesStageData>
{
    public NegativesStage(SurveyState state, INegativesStageData data, IEntitiesData entities)
        : base(state.NegativesStage, data, entities)
    {
    }

    public Question? Question { get; private set; }

    public override void Init()
    {
        var negatives = entities.Negatives.Where(n => state.Types.Any(t => n.TypeId == t.Id));

        Question = new(
            data.NegativesQuestionLabel,
            negatives.Select(n => new Option(n.Id, n.Name)));
    }

    public override void Save()
    {
        var selectedNegatives = Question!.Selected.Select(n => n.Id);
        state.SetNegatives(entities.Negatives.Where(n => selectedNegatives.Contains(n.Id)));
    }

    public override bool CanMoveNext() => Question!.Selected.Count > 0;
    public override bool MoveNext() => false;
}
