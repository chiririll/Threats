namespace Threats.Models.Questions;

public class Option(int id, string label)
{
    public Option() : this(0, string.Empty)
    {
    }

    public int Id { get; } = id;
    public string Label { get; } = label;
    public bool Selected { get; set; } = false;
}
