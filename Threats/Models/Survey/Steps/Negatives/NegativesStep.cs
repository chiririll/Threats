using System.Linq;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativesStep : SurveyStep<NegativesStepState, INegativesStepData>
{
    private readonly Question typesQuestion;
    private Question? negativesQuestion;

    public NegativesStep(SurveyState state, INegativesStepData data) : base(state.NegativesStep, data)
    {
        typesQuestion = new(data.TypesQuestionLabel, data.NegativeTypes.Select(t => new Option(t.Id, t.Name)), "afs");
        negativesQuestion = null;

        CurrentQuestion = typesQuestion;
    }

    public Question CurrentQuestion { get; private set; }

    public override void Save()
    {
        var selectedTypes = typesQuestion.Selected.Select(t => t.Id);
        state.SetTypes(data.NegativeTypes.Where(t => selectedTypes.Contains(t.Id)));

        if (negativesQuestion != null)
        {
            var selectedNegatives = negativesQuestion.Selected.Select(n => n.Id);
            state.SetNegatives(data.Negatives.Where(n => selectedNegatives.Contains(n.Id)));
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

        var negatives = data.Negatives.Where(n => state.Types.Contains(n.Type));

        negativesQuestion = new(
            data.NegativesQuestionLabel,
            negatives.Select(n => new Option(n.Id, n.Name)));
        CurrentQuestion = negativesQuestion;

        return true;
    }
}
