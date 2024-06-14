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
    private readonly SurveyData surveyData;
    private readonly EntitiesData entities;
    private readonly QuestionsData questions;

    private readonly CompositeDisposable pageSub = new();

    private ViewModelBase content;

    private SurveyManager? survey;

    public MainWindowViewModel()
    {
        surveyData = DataLoader.LoadSurveyData();
        entities = DataLoader.LoadEntitiesData();
        questions = DataLoader.LoadQuestionsData();

        ShowStart();

        if (content == null)
        {
            throw new NullReferenceException();
        }
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

    public void ShowStart()
    {
        var startPage = new StartPageViewModel();
        Content = startPage;

        startPage.StartSurvey
            .Subscribe(_ => StartSurvey())
            .AddTo(pageSub);
    }

    public void StartSurvey()
    {
        survey = new(surveyData, entities, questions);

        var surveyPage = new SurveyPageViewModel(survey);
        Content = surveyPage;

        surveyPage.OnComplete
            .Subscribe(ShowResult)
            .AddTo(pageSub);
    }

    public void ShowResult(SurveyResult result)
    {
        var resultPage = new ResultPageViewModel(result, entities);
        Content = resultPage;

        resultPage.OnRestartRequested
            .Subscribe(_ => ShowStart())
            .AddTo(pageSub);
    }
}
