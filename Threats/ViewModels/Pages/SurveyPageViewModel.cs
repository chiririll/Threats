using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Survey;

namespace Threats.ViewModels.Pages;

public class SurveyPageViewModel : ViewModelBase
{
    private readonly Subject<bool> canMoveNext = new();
    private readonly Subject<SurveyResult> onComplete = new();

    private readonly SurveyManager survey;

    public SurveyPageViewModel(SurveyManager survey)
    {
        this.survey = survey;

        StageContainer.SetStage(survey.CurrentStage!);
        StageContainer.Updated.Subscribe(_ => UpdateMoveNextButtonState());

        Submit = ReactiveCommand.Create(MoveToNextStage, canMoveNext);
        UpdateMoveNextButtonState();
    }

    public IObservable<SurveyResult> OnComplete => onComplete;
    public IObservable<Unit> Submit { get; }

    public string Title => survey.Title;
    public StageViewModel StageContainer { get; } = new();

    private void UpdateMoveNextButtonState()
    {
        canMoveNext.OnNext(survey.CanMoveNext());
    }

    private void MoveToNextStage()
    {
        if (!survey.MoveToNextStage())
        {
            onComplete.OnNext(survey.GetResult());
            return;
        }

        StageContainer.SetStage(survey.CurrentStage!);
        this.RaisePropertyChanged(nameof(Title));

        UpdateMoveNextButtonState();
    }
}
