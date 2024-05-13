using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class ThreatsStepData : StepData, IThreatsStepData
{
    public ThreatsStepData(string title) : base(title)
    {
    }
}
