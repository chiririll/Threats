using System;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.Models.Survey.Data;
using Threats.ViewModels.Pages;

namespace Threats.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase content;

    private SurveyManager? survey;

    public MainWindowViewModel()
    {
        var startPage = new StartPageViewModel();

        startPage
            .StartSurvey
            .Subscribe(_ => StartSurvey());

        content = startPage;
    }

    public ViewModelBase Content
    {
        get => content;
        private set => this.RaiseAndSetIfChanged(ref content, value);
    }

    public void StartSurvey()
    {
        var data = new DatabaseSurveyData();
        survey = new(data);

        Content = new SurveyPageViewModel(survey);
    }
}
