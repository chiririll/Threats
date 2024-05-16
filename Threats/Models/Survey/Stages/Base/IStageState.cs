namespace Threats.Models.Survey;

public interface IStageState
{
    public void FillResult(SurveyResultBuilder builder);
}
