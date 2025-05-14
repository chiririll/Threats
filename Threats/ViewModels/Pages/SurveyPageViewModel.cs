using System;
using System.Reactive;
using System.Reactive.Subjects;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Survey;

namespace Threats.ViewModels.Pages;

public class SurveyPageViewModel : ViewModelBase
{
    private const int DefaultMaxWidth = 1100;

    private readonly Subject<bool> canMoveNext = new();
    private readonly Subject<SurveyResult> onComplete = new();

    private readonly SurveyManager survey;

    private bool completed = false;

    public SurveyPageViewModel(SurveyManager survey)
    {
        this.survey = survey;

        StageContainer.SetStage(survey.CurrentStage!);
        StageContainer.Updated.Subscribe(_ => UpdateMoveNextButtonState());

        Submit = ReactiveCommand.Create(MoveToNextStage, canMoveNext);
        GoBack = ReactiveCommand.Create(MoveToPrevStage);

        UpdateMoveNextButtonState();
    }

    public IObservable<SurveyResult> OnComplete => onComplete;
    public IObservable<Unit> Submit { get; }
    public IObservable<Unit> GoBack { get; }

    public bool CanMoveBack => survey.CanMoveBack();

    public string Title => survey.Title;
    public StageViewModel StageContainer { get; } = new();
    public int MaxWidth => StageContainer.MaxWidth ?? DefaultMaxWidth;

    private void UpdateMoveNextButtonState()
    {
        canMoveNext.OnNext(survey.CanMoveNext());
    }

    private void Refresh()
    {
        StageContainer.SetStage(survey.CurrentStage!);
        this.RaisePropertyChanged(nameof(Title));
        this.RaisePropertyChanged(nameof(CanMoveBack));
        this.RaisePropertyChanged(nameof(MaxWidth));

        UpdateMoveNextButtonState();
    }

    private void MoveToNextStage()
    {
        if (!survey.MoveToNextStage())
        {
            TryCompleteSurvey();
            return;
        }

        Refresh();
    }

    public async void TryCompleteSurvey()
    {
        if (completed)
            return;
        completed = true;

        var completeAlert = MessageBoxManager
           .GetMessageBoxStandard(
                "Подтверждение",
                "Вы готовы сформировать список актуальных угроз?",
               ButtonEnum.YesNo);

        var result = await completeAlert.ShowAsync();
        if (result == ButtonResult.Yes)
        {
            onComplete.OnNext(survey.GetResult());
            return;
        }

        completed = false;
    }

    private void MoveToPrevStage()
    {
        if (!survey.MoveToPreviousStage())
        {
            return;
        }

        Refresh();
    }
}
