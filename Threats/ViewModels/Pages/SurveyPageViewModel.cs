using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.Models.Survey.Data;
using Threats.ViewModels.Survey;

namespace Threats.ViewModels.Pages;

public class SurveyPageViewModel : ViewModelBase
{
    private readonly Subject<bool> canMoveNext = new();
    private readonly Subject<SurveyManager> onComplete = new();

    private readonly SurveyManager survey;

    public SurveyPageViewModel()
    {
        survey = new(new DummySurveyData());

        StepContainer.SetStep(survey.CurrentStep!);
        StepContainer.Updated.Subscribe(_ => UpdateMoveNextButtonState());

        Submit = ReactiveCommand.Create(MoveToNextStep, canMoveNext);
        UpdateMoveNextButtonState();
    }

    public IObservable<SurveyManager> OnComplete => onComplete;
    public IObservable<Unit> Submit { get; }

    public string Title => survey.Title;
    public StepViewModel StepContainer { get; } = new();

    private void UpdateMoveNextButtonState()
    {
        canMoveNext.OnNext(survey.CanMoveNext());
    }

    private void MoveToNextStep()
    {
        if (!survey.MoveToNextStep())
        {
            onComplete.OnNext(survey);
            return;
        }

        StepContainer.SetStep(survey.CurrentStep!);
        this.RaisePropertyChanged(nameof(Title));

        UpdateMoveNextButtonState();
    }
}
