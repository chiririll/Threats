using Threats.Data.Questions;

namespace Threats.Models.Questions;

public class Option
{
    public Option(OptionData data)
    {
        Id = data.Id;
        Group = data.Group ?? string.Empty;

        Label = new(data.Label, data.HelpText);

        Payload = data.Payload;
    }

    public Option(int id, string label, string? group = null, string? helpText = null, IOptionPayload? payload = null)
    {
        Id = id;
        Label = new(label, helpText);
        Group = group ?? string.Empty;
        Payload = payload;
    }

    public Option() : this(0, string.Empty)
    {
    }

    public IOptionPayload? Payload { get; }

    public int Id { get; }
    public string Group { get; }

    public LabelWithDescription Label { get; }

    public bool Selected { get; set; } = false;
}
