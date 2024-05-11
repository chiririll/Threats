using System.Collections.Generic;
using Threats.Models.Entities;

namespace Threats.Models.Survey.Data;

public interface INegativesStepData : IStepData
{
    public string TypesQuestionLabel { get; }
    public IEnumerable<NegativeType> NegativeTypes { get; }

    public string NegativesQuestionLabel { get; }
    public IEnumerable<Negative> Negatives { get; }
}
