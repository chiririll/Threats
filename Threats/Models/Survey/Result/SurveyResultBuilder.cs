using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResultBuilder
{
    private IEnumerable<Negative>? negatives;

    public SurveyResultBuilder WithNegatives(IEnumerable<Negative> negatives)
    {
        this.negatives = negatives;

        return this;
    }

    public SurveyResult Build() => new SurveyResult(negatives!);
}
