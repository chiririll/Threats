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

    public ObjectsOptionData Build() => new(
        id,
        GroupPrefix + questionId.ToString(),
        title ?? string.Empty,
        helpText,
        objectsToAdd);
}
