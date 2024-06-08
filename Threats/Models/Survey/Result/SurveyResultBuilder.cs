using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResultBuilder
{
    private IEnumerable<Negative>? negatives;
    private IEnumerable<Object>? objects;
    private IEnumerable<Intruder>? intruders;
    private IEnumerable<Threat>? threats;

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

    public SurveyResultBuilder WithThreats(IEnumerable<Threat> threats)
    {
        this.threats = threats;

        return this;
    }

    public SurveyResult Build() => new(negatives!, objects!, intruders!, threats!);
}
