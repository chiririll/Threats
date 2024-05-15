using Threats.Data.Questions;

namespace Threats.Models.Questions;

public class Option
{

    public Option(OptionData data)
    {
        Id = data.Id;
        Group = data.Group ?? string.Empty;

        Label = new(data.Label, data.HelpText);

        Data = data;
    }

    public Option(int id, string label, string? group = null)
    {
        Id = id;
        Label = new(label, null);
        Group = group ?? string.Empty;

        Data = new(id, Group, label, null);
    }

    public Option() : this(0, string.Empty)
    {
    }

    public OptionData Data { get; }

    public int Id { get; }
    public string Group { get; }

    public LabelWithDescription Label { get; }

    public bool Selected { get; set; } = false;
}
