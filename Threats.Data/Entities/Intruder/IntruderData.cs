using System.Collections.Generic;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class IntruderData : Entity
{
    [JsonProperty("goals", Order = 10)] private readonly List<string> goals;
    [JsonProperty("negatives", Order = 11)] private readonly List<int> negatives;

    public IntruderData(int id, string title, IntruderPotential potential, IEnumerable<string> goals, IEnumerable<int> negatives) : base(id)
    {
        Title = title;
        Potential = potential;

        this.goals = new(goals);
        this.negatives = new(negatives);
    }

    [JsonProperty("title")] public string Title { get; }
    [JsonProperty("potential")] public IntruderPotential Potential;

    [JsonIgnore] public IReadOnlyList<string> Goals => goals;
    [JsonIgnore] public IReadOnlyList<int> Negatives => negatives;
}
