using Newtonsoft.Json;

namespace Threats.Data.Questions;

public class OptionData
{
    public OptionData(int id, string? group, string label, string? helpText)
    {
        Id = id;
        Group = group;
        Label = label;
        HelpText = helpText;
    }

    [JsonProperty("id")] public int Id { get; }
    [JsonProperty("group")] public string? Group { get; }
    [JsonProperty("label")] public string Label { get; }
    [JsonProperty("help_text")] public string? HelpText { get; }
}
