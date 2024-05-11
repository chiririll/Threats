using System.Collections.Generic;

namespace Threats.Models.Questions;

public class Question
{
    private readonly List<Option> options = new();

    public Question(string label, IEnumerable<Option> options, string? description = null) : this(new(label, description), options)
    {
    }

    public Question(LabelWithDescription label, IEnumerable<Option> options)
    {
        Label = label;

        this.options = new(options);
    }

    public LabelWithDescription Label { get; }
    public IEnumerable<Option> Options => options;

    public IReadOnlyList<Option> Selected => options.FindAll(opt => opt.Selected);
}
