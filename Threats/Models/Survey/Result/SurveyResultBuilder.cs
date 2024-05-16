using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResultBuilder
{
    private IEnumerable<Negative>? negatives;
    private IEnumerable<Object>? objects;
    private IEnumerable<Intruder>? intruders;

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

    public SurveyResultBuilder WithIntruders(IEnumerable<Intruder> intruders)
    {
        this.intruders = intruders;

        return this;
    }

    public SurveyResult Build(IEntitiesData entities) => new(entities, negatives!, objects!, intruders!);
}
