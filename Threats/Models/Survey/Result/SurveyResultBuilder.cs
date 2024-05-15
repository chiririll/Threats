using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResultBuilder
{
    private IEnumerable<Negative>? negatives;
    private IEnumerable<Object>? objects;

    public SurveyResultBuilder WithNegatives(IEnumerable<Negative> negatives)
    {
        this.negatives = negatives;

        return this;
    }

    public SurveyResultBuilder WithObjects(IEnumerable<Object> objects)
    {
        this.objects = objects;

        return this;
    }

    public SurveyResult Build() => new SurveyResult(negatives!, objects!);
}
