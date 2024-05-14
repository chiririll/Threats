using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class IntruderData : Entity
{
    [JsonProperty("goals", Order = 10)] private readonly List<string> goals;

    public IntruderData(int id, string title, IntruderPotential potential, IEnumerable<string> goals) : base(id)
    {
        Title = title;
        Potential = potential;

        this.goals = new(goals);
    }

    [JsonProperty("title")] public string Title { get; }
    [JsonProperty("potential")] public IntruderPotential Potential;

    [JsonIgnore] public IReadOnlyList<string> Goals => goals;
}
