using Newtonsoft.Json;

namespace Threats.Models.Survey.Data.Base;

public abstract class StageData : IStageData
{
    public StageData(string title)
    {
        Title = title;
    }

    [JsonProperty("title")] public string Title { get; }
}
