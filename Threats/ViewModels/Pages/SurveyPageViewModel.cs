using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using Threats.Models.Survey;
using Threats.ViewModels.Survey;

namespace Threats.ViewModels.Pages;

public class SurveyPageViewModel : ViewModelBase, IDisposable
{
    private const int DefaultMaxWidth = 1100;

    private readonly Subject<bool> canMoveNext = new();
    private readonly Subject<SurveyResult> onComplete = new();
    private readonly Subject<Unit> onCompletionRequested = new();
    private readonly CompositeDisposable disp = new();

    private readonly SurveyManager survey;

    public SurveyPageViewModel(SurveyManager survey)
    {
        this.survey = survey;

        StageContainer = new(survey);
        StageContainer.SetStage(survey.CurrentStage!);
        StageContainer.Updated.Subscribe(_ => UpdateMoveNextButtonState());

        Submit = ReactiveCommand.Create(MoveToNextStage, canMoveNext);
        GoBack = ReactiveCommand.Create(MoveToPrevStage);
        InvokeAction = ReactiveCommand.Create(CallInvokeAction);

        UpdateMoveNextButtonState();

        survey.OnStateChanged
            .Subscribe(_ => Refresh())
            .AddTo(disp);
    }

    public IObservable<SurveyResult> OnComplete => onComplete;
    public IObservable<Unit> OnCompletionRequested => onCompletionRequested;

    public IObservable<Unit> Submit { get; }
    public IObservable<Unit> GoBack { get; }

    public bool CanMoveBack => survey.CanMoveBack();

    public bool HasAction => !string.IsNullOrEmpty(StageContainer.Stage.ActionName);
    public string ActionName => StageContainer.Stage.ActionName ?? string.Empty;
    public IObservable<Unit> InvokeAction { get; }

    public string Title => survey.Title;
    public StageViewModel StageContainer { get; private set; }
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

        this.RaisePropertyChanged(nameof(HasAction));
        this.RaisePropertyChanged(nameof(ActionName));

        UpdateMoveNextButtonState();
    }

    private void MoveToNextStage()
    {
        if (survey.MoveToNextStage())
            return;

        TryCompleteSurvey();
    }

    public void TryCompleteSurvey()
    {
        onCompletionRequested.OnNext(default);
    }

    public void CompleteSurvey()
    {
        onComplete.OnNext(survey.GetResult());
    }

    private void MoveToPrevStage()
    {
        survey.MoveToPreviousStage();
    }

    private void CallInvokeAction()
    {
        StageContainer.Stage.InvokeAction();
    }

    public void Dispose()
    {
        disp.Dispose();
    }
}
