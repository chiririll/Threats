using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using ReactiveUI;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public sealed class StepViewModel : ViewModelBase, IDisposable
{
    private readonly Subject<Unit> updated = new();
    private readonly CompositeDisposable stepSub = new();

    private SurveyStepViewModel? step;

    public StepViewModel()
    {
    }

    public SurveyStepViewModel Step
    {
        get => step!;
        private set => this.RaiseAndSetIfChanged(ref step, value);
    }

    public IObservable<Unit> Updated => updated;

    public void SetStep(SurveyStep step) => SetStep(SurveyStepViewModelFactory.Create(step));
    public void SetStep(SurveyStepViewModel step)
    {
        stepSub.Clear();
        Step = step;

        step.Updated
            .Subscribe(_ => updated.OnNext(_))
            .AddTo(stepSub);
    }

    public void Refresh()
    {
        step?.Refresh();
    }

    public void Dispose()
    {
        stepSub.Dispose();
    }
}
