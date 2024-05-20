using Newtonsoft.Json;

namespace Threats.Models.Survey.Data.Base;

public abstract class StageData : IStageData
{
    [JsonProperty("title")] public string Title { get; private set; } = string.Empty;
}
