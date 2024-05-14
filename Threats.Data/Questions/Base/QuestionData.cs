using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Questions;

public class QuestionData
{
    [JsonProperty("options", Order = 10)] private readonly List<OptionData> options;

    public QuestionData(int id, string title, string? helpText, IEnumerable<OptionData> options)
    {
        Id = id;

        Title = title;
        HelpText = helpText;

        this.options = new(options);
    }

    [JsonProperty("id")] public int Id { get; }

    [JsonProperty("title")] public string Title { get; }
    [JsonProperty("help_text")] public string? HelpText { get; }

    [JsonIgnore] public IReadOnlyList<OptionData> Options => options;
}
