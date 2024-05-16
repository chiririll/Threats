using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey.State;

public class ObjectsStageState : IStageState
{
    private readonly List<Object> objects = new();

    public IReadOnlyList<Object> Objects => objects;

    public void SetObjects(IEnumerable<Object> objects)
    {
        this.objects.Clear();
        this.objects.AddRange(objects);
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithObjects(objects);
    }
}
