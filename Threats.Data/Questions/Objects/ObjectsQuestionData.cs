using System.Collections.Generic;

namespace Threats.Data.Questions.Objects;

public class ObjectsQuestionData : IQuestionData<ObjectsOptionData>
{
    public ObjectsQuestionData(int id, string title, string? helpText, IEnumerable<ObjectsOptionData> options)
    {
    }

    public int Id => throw new System.NotImplementedException();

    public string Title => throw new System.NotImplementedException();

    public string? HelpText => throw new System.NotImplementedException();

    public IReadOnlyList<ObjectsOptionData> Options => throw new System.NotImplementedException();
}
