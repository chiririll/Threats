using Newtonsoft.Json;

namespace Threats.Models.Survey.Data.Base;

public abstract class StepData : IStepData
{
    public StepData(string title)
    {
        Title = title;
    }

    [JsonProperty("title")] public string Title { get; }
}
