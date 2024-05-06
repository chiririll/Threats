using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Survey;

namespace Threats.ViewModels.Pages;

public class SurveyPageViewModel : ViewModelBase
{
    private readonly SurveyManager survey;

    private readonly Subject<SurveyManager> onComplete = new();

    private SurveyStepViewModel stepViewModel;

    public SurveyPageViewModel()
    {
        survey = new();

        stepViewModel = SurveyStepViewModelFactory.Create(survey.CurrentStep!);

        Submit = ReactiveCommand.Create(MoveToNextStep, survey.CanSubmitCurrentStep);
    }

    public IObservable<SurveyManager> OnComplete => onComplete;
    public IObservable<Unit> Submit { get; }

    public SurveyStepViewModel StepViewModel
    {
        get => stepViewModel;
        private set => this.RaiseAndSetIfChanged(ref stepViewModel, value);
    }

    private void MoveToNextStep()
    {
        if (!survey.MoveToNextStep())
        {
            onComplete.OnNext(survey);
        }
    }
}
