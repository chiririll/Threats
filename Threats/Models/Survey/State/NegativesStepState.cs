using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey.State;

public class NegativesStepState
{
    private readonly List<NegativeType> types = new();
    private readonly List<Negative> negatives = new();

    public IReadOnlyList<NegativeType> Types => types;
    public IReadOnlyList<Negative> Negatives => negatives;

    public void SetTypes(IEnumerable<NegativeType> types)
    {
        this.types.Clear();
        this.types.AddRange(types);
    }

    public void SetNegatives(IEnumerable<Negative> negatives)
    {
        this.negatives.Clear();
        this.negatives.AddRange(negatives);
    }
}
