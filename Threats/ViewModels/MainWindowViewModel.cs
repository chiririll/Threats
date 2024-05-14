using System;
using System.Reactive.Disposables;
using ReactiveUI;
using Threats.Data;
using Threats.Models.Survey;
using Threats.Models.Survey.Data;
using Threats.ViewModels.Pages;

namespace Threats.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly EntitiesData entities;
    private readonly SurveyData surveyData;

    private readonly CompositeDisposable pageSub = new();

    private ViewModelBase content;

    private SurveyManager? survey;

    public MainWindowViewModel()
    {
        entities = DataLoader.LoadEntitiesData();
        surveyData = DataLoader.LoadSurveyData();

        var startPage = new StartPageViewModel();

        startPage
            .StartSurvey
            .Subscribe(_ => StartSurvey())
            .AddTo(pageSub);

        content = startPage;
    }

    public ViewModelBase Content
    {
        get => content;
        private set
        {
            if (value == content)
                return;

            pageSub.Clear();
            this.RaiseAndSetIfChanged(ref content, value);
        }
    }

    public void StartSurvey()
    {
        survey = new(surveyData, entities);

        var surveyPage = new SurveyPageViewModel(survey);
        Content = surveyPage;

        surveyPage
            .OnComplete
            .Subscribe(ShowResult)
            .AddTo(pageSub);
    }

    public void ShowResult(SurveyResult result)
    {
        Content = new ResultPageViewModel(result);
    }
}
