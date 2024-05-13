namespace Threats.Models.Survey.Data;

public interface INegativesStepData : IStepData
{
    public string TypesQuestionLabel { get; }
    public string NegativesQuestionLabel { get; }
}
