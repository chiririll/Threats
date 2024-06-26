using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey.State;

public class NegativesStageState : IStageState
{
    private readonly List<NegativeType> types = new();
    private readonly List<Negative> negatives = new();

    public IReadOnlyList<NegativeType> Types => types;
    public IReadOnlyList<Negative> Negatives => negatives;

    public bool HasType(int typeId) => types.Find(t => t.Id.Equals(typeId)) != null;

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

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithNegatives(negatives);
    }
}
