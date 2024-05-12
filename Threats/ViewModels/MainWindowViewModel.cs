using System;
using ReactiveUI;
using Threats.Data;
using Threats.Models.Survey;
using Threats.Models.Survey.Data;
using Threats.ViewModels.Pages;

namespace Threats.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly EntitiesData entities;

    private ViewModelBase content;

    private SurveyManager? survey;

    public MainWindowViewModel()
    {
        entities = DataLoader.LoadEntitiesData();

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
        var data = new SurveyData(entities);
        survey = new(data);

        Content = new SurveyPageViewModel(survey);
    }
}
