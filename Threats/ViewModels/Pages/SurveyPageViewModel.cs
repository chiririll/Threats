using System;
using System.Reactive;
using System.Reactive.Subjects;
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
            onComplete.OnNext(survey.GetResult());
            return;
        }

        Refresh();
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
