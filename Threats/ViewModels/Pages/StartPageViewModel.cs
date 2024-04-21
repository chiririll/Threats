using System;
using System.Reactive;
using ReactiveUI;

namespace Threats.ViewModels.Pages;

public class StartPageViewModel : ViewModelBase
{
    public StartPageViewModel()
    {
        StartPoll = ReactiveCommand.Create(() => { });
    }

    public IObservable<Unit> StartPoll { get; }
}
