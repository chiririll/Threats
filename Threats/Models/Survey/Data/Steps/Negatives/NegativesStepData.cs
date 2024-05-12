using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Survey.Data;

public class NegativesStepData : INegativesStepData
{
    private readonly EntitiesData data;

    public NegativesStepData(EntitiesData data)
    {
        this.data = data;
    }

    public string Title => "TODO";

    public string TypesQuestionLabel => "TODO";
    public string NegativesQuestionLabel => "TODO";

    public IEnumerable<NegativeType> NegativeTypes => data.NegativeTypes;
    public IEnumerable<Negative> Negatives => data.Negatives;
}
