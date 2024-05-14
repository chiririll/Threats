using System.Linq;
using Threats.Data;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativesStage : SurveyStage<NegativesStageState, INegativesStageData>
{
    private readonly Question typesQuestion;
    private Question? negativesQuestion;

    public NegativesStage(SurveyState state, INegativesStageData data, IEntitiesData entities)
        : base(state.NegativesStage, data, entities)
    {
        typesQuestion = new(data.TypesQuestionLabel, entities.NegativeTypes.Select(t => new Option(t.Id, t.Name)), "Подсказка");
        negativesQuestion = null;

        CurrentQuestion = typesQuestion;
    }

    public Question CurrentQuestion { get; private set; }

    public override void Save()
    {
        var selectedTypes = typesQuestion.Selected.Select(t => t.Id);
        state.SetTypes(entities.NegativeTypes.Where(t => selectedTypes.Contains(t.Id)));

        if (negativesQuestion != null)
        {
            var selectedNegatives = negativesQuestion.Selected.Select(n => n.Id);
            state.SetNegatives(entities.Negatives.Where(n => selectedNegatives.Contains(n.Id)));
        }
    }

    public override bool CanMoveNext() => CurrentQuestion.Selected.Count > 0;

    public override bool MoveNext()
    {
        if (!CanMoveNext())
        {
            return true;
        }

        if (negativesQuestion != null)
        {
            return false;
        }

        var negatives = entities.Negatives.Where(n => state.HasType(n.TypeId));

        negativesQuestion = new(
            data.NegativesQuestionLabel,
            negatives.Select(n => new Option(n.Id, n.Name)));
        CurrentQuestion = negativesQuestion;

        return true;
    }
}
