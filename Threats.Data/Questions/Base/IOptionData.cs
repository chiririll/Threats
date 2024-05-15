namespace Threats.Data.Questions;

public interface IOptionData
{
    public int Id { get; }
    public string? Group { get; }
    public string Label { get; }
    public string? HelpText { get; }
}
