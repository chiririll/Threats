namespace Threats.Models.Questions;

public class Option
{
    public Option(int id, string label, string? group = null)
    {
        Id = id;
        Label = label;
        Group = group ?? string.Empty;
    }

    public Option() : this(0, string.Empty)
    {
    }

    public int Id { get; }
    public string Group { get; }
    public string Label { get; }

    public bool Selected { get; set; } = false;
}
