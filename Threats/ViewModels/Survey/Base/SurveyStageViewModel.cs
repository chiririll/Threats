using System;
using System.Reactive;
using System.Reactive.Subjects;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public abstract class SurveyStageViewModel<TStage> : SurveyStageViewModel
    where TStage : SurveyStage
{
    protected readonly TStage stage;

    public SurveyStageViewModel(TStage stage)
    {
        this.stage = stage;
    }
}

public abstract class SurveyStageViewModel : ViewModelBase, IDisposable
{
    protected readonly Subject<Unit> updated = new();

    public IObservable<Unit> Updated => updated;

    public abstract void Refresh();

    public virtual void Dispose()
    {
    }
}
