namespace Threats.Models.Survey.Data;

public interface INegativesStageData : IStageData
{
    public string TypesQuestionLabel { get; }
    public string NegativesQuestionLabel { get; }
}
