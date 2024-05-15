using System.Collections.Generic;
using Threats.Data.Questions;

namespace Threats.Parser.ObjectsList;

public class OptionBuilder
{
    private const string GroupPrefix = "obj_";

    private readonly int id;
    private readonly int questionId;

    private string? title;
    private string? helpText;

    private readonly HashSet<int> objectsToAdd = new();
    private readonly HashSet<int> objectsToRemove = new();

    public OptionBuilder(int id, int questionId)
    {
        this.id = id;
        this.questionId = questionId;
    }

    public OptionBuilder WithTitle(string title)
    {
        this.title = title;
        return this;
    }

    public OptionBuilder SetHelpText(string text)
    {
        helpText = text;
        return this;
    }

    public OptionBuilder AddObject(int id)
    {
        objectsToAdd.Add(id);
        return this;
    }

    public OptionBuilder AddObjects(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            objectsToAdd.Add(id);
        }

        return this;
    }

    public OptionBuilder ExcludeObject(int id)
    {
        objectsToRemove.Add(id);
        return this;
    }

    public OptionBuilder ExcludeObjects(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            objectsToRemove.Add(id);
        }

        return this;
    }

    public OptionData Build() => new(
        id,
        GroupPrefix + questionId.ToString(),
        title ?? string.Empty,
        helpText,
        new ObjectsOptionPayload(objectsToAdd, objectsToRemove));
}
