using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey.State;

public class ObjectsStageState : IStageState
{
    private readonly List<Object> selectedObjects = new();
    private readonly List<Object> appendedObjects = new();
    private readonly List<Object> importedObjects = new();
    private readonly List<Object> allObjects = new();

    public IReadOnlyList<Object> Objects => allObjects;
    public bool HasImportedObjects => importedObjects.Count > 0;

    public void SetObjects(IEnumerable<Object> objects)
    {
        selectedObjects.Clear();
        selectedObjects.AddRange(objects);

        UpdateAllObjects();
    }

    public void ImportObjects(IEnumerable<Object> objects)
    {
        importedObjects.Clear();
        importedObjects.AddRange(objects);

        UpdateAllObjects();
    }

    public bool AddObject(Object obj)
    {
        if (allObjects.Find(o => o.Id.Equals(obj.Id)) != null)
        {
            return false;
        }

        appendedObjects.Add(obj);
        allObjects.Add(obj);

        return true;
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        UpdateAllObjects();
        builder.WithObjects(allObjects);
    }

    private void UpdateAllObjects()
    {
        void AddCollection(IEnumerable<Object> collection)
        {
            foreach (var obj in collection)
            {
                if (allObjects.Find(o => o.Id.Equals(obj.Id)) != null)
                    continue;

                allObjects.Add(obj);
            }
        }

        allObjects.Clear();

        allObjects.AddRange(importedObjects);
        AddCollection(selectedObjects);
        AddCollection(appendedObjects);
    }
}
