using System;
using System.Reactive;
using System.Reactive.Subjects;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public abstract class SurveyStepViewModel<TStep> : SurveyStepViewModel
    where TStep : SurveyStep
{
    protected readonly TStep step;

    public SurveyStepViewModel(TStep step)
    {
        this.step = step;
    }
}

public abstract class SurveyStepViewModel : ViewModelBase, IDisposable
{
    protected readonly Subject<Unit> updated = new();

    public IObservable<Unit> Updated => updated;

    public abstract void Refresh();

    public virtual void Dispose()
    {
    }
}
