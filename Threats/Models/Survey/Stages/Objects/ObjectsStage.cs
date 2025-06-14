using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Data.Questions;
using Threats.Models.Objects.Importers;
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

    public override StageType Type => StageType.Objects;

    public override void Init()
    {
    }

    public override void Save()
    {
        if (questions.Find(q => q.Selected.Count == 0) != null)
            return;

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

    public override bool CanMoveNext() => state.HasImportedObjects || questions.Find(q => q.Selected.Count == 0) == null;
    public override bool MoveNext() => false;

    public override bool CanMoveBack() => false;
    public override bool MoveBack() => false;

    public bool ImportFiles(IEnumerable<string> files)
    {
        var importers = GetImporters();
        var addedObjects = new HashSet<Object>();

        foreach (var file in files)
        {
            var result = new List<Object>();
            if (!importers.Any(i => i.Import(file, ref result)))
                continue;

            result.ForEach(obj => addedObjects.Add(obj));
        }

        if (addedObjects.Count < 1)
            return false;

        state.ImportObjects(addedObjects);
        return true;
    }

    private List<IObjectsImporter> GetImporters()
    {
        var baseImporter = typeof(IObjectsImporter);
        var importers = GetType().Assembly.GetTypes()
            .Where(t => !t.IsAbstract && baseImporter.IsAssignableFrom(t))
            .Select(t => (IObjectsImporter)System.Activator.CreateInstance(t)!)
            .ToList();

        importers.ForEach(i => i.Init(entities));
        return importers;
    }
}
