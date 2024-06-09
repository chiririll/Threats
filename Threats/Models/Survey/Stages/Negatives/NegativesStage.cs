using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class NegativesStage : SurveyStage<NegativesStageState, INegativesStageData>
{
    private readonly List<Question> questions = new();

    public NegativesStage(SurveyState state, INegativesStageData data, IEntitiesData entities)
        : base(state.NegativesStage, data, entities)
    {
    }

    public IReadOnlyList<Question> Questions => questions;

    public override StageType Type => StageType.Negatives;

    public override void Init()
    {
        questions.Clear();

        foreach (var type in state.Types)
        {
            var negatives = entities.Negatives.Where(n => n.TypeId == type.Id);
            var question = new Question(type.Name, negatives.Select(
                n => new Option(n.Id, n.Name)
                {
                    Selected = state.Negatives.Contains(n)
                }), type.Description);
            questions.Add(question);
        }
    }

    public override void Save()
    {
        var selectedNegatives = questions.SelectMany(q => q.Selected.Select(n => n.Id));
        state.SetNegatives(entities.Negatives.Where(n => selectedNegatives.Contains(n.Id)));
    }

    public override bool CanMoveNext() => questions.Any(q => q.Selected.Count > 0);
    public override bool MoveNext() => false;

    public override bool CanMoveBack() => false;
    public override bool MoveBack() => false;
}
