namespace Threats.Models.Questions;

public class Option
{
    public Option(int id, string label)
    {
        Id = id;
        Label = label;
    }

    public Option() : this(0, string.Empty)
    {
    }

    public int Id { get; }
    public string Label { get; }
    public bool Selected { get; set; } = false;
}
