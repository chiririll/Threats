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
    private readonly SurveyManager survey;

    private readonly Subject<SurveyManager> onComplete = new();

    public SurveyPageViewModel()
    {
        survey = new(new DummySurveyData());

        StepContainer = new(SurveyStepViewModelFactory.Create(survey.CurrentStep!));

        Submit = ReactiveCommand.Create(MoveToNextStep, survey.CanSubmitCurrentStep);
    }

    public IObservable<SurveyManager> OnComplete => onComplete;
    public IObservable<Unit> Submit { get; }

    public string Title => survey.Title;
    public StepViewModel StepContainer { get; }

    private void MoveToNextStep()
    {
        if (!survey.MoveToNextStep())
        {
            onComplete.OnNext(survey);
        }
    }
}
