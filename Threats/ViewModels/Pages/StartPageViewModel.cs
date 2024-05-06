using System;
using System.Reactive;
using ReactiveUI;

namespace Threats.ViewModels.Pages;

public class StartPageViewModel : ViewModelBase
{
    public StartPageViewModel()
    {
        StartSurvey = ReactiveCommand.Create(() => { });
    }

    public IObservable<Unit> StartSurvey { get; }
}
