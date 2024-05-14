using Threats.Models.Survey;

namespace Threats.ViewModels.Pages;

public class ResultPageViewModel : ViewModelBase
{
    public ResultPageViewModel(SurveyResult result)
    {
        Result = result;
    }

    public SurveyResult Result { get; }
}
