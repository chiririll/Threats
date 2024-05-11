namespace Threats.Models.Questions;

public class LabelWithDescription
{
    public LabelWithDescription(string label, string? description = null)
    {
        Label = label;
        Description = description ?? string.Empty;
        HasDescription = !string.IsNullOrWhiteSpace(description);
    }

    public string Label { get; }
    public string Description { get; }
    public bool HasDescription { get; }
}
