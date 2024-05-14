using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using ReactiveUI;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public sealed class StageViewModel : ViewModelBase, IDisposable
{
    private readonly Subject<Unit> updated = new();
    private readonly CompositeDisposable stageSub = new();

    private SurveyStageViewModel? stage;

    public StageViewModel()
    {
    }

    public SurveyStageViewModel Stage
    {
        get => stage!;
        private set => this.RaiseAndSetIfChanged(ref stage, value);
    }

    public IObservable<Unit> Updated => updated;

    public void SetStage(SurveyStage stage) => SetStage(SurveyStageViewModelFactory.Create(stage));
    public void SetStage(SurveyStageViewModel stage)
    {
        stageSub.Clear();
        Stage = stage;

        stage.Updated
            .Subscribe(_ => updated.OnNext(_))
            .AddTo(stageSub);
    }

    public void Refresh()
    {
        stage?.Refresh();
    }

    public void Dispose()
    {
        stageSub.Dispose();
    }
}
