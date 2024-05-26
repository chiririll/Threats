using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Data.Questions;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ObjectsStage : SurveyStage<ObjectsStageState, IObjectsStageData>
{
    private readonly List<Question> questions;

    public ObjectsStage(
        SurveyState state,
        IObjectsStageData data,
        IEntitiesData entities,
        IQuestionsData questions) : base(state.ObjectsStage, data, entities)
    {
        this.questions = questions.ObjectsQuestions.Select(q => new Question(q)).ToList();
    }

    public IReadOnlyList<Question> Questions => questions;

    public override void Init()
    {
    }

    public override void Save()
    {
        var selectedOptions = questions.SelectMany(q => q.Selected.Select(o => o.Payload).OfType<ObjectsOptionPayload>());

        var included = selectedOptions.SelectMany(o => o.ObjectsToAdd);
        var excluded = selectedOptions.SelectMany(o => o.ObjectsToExclude);

        var objects = new List<Object>();
        foreach (var objectId in included.Except(excluded))
        {
            var obj = entities.GetObjectById(objectId);
            if (obj == null)
                continue;
            objects.Add(obj);
        }

        state.SetObjects(objects);
    }

    public override bool CanMoveNext() => questions.Find(q => q.Selected.Count == 0) == null;
    public override bool MoveNext() => false;
}
