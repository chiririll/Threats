using System;
using System.Reactive;
using System.Reflection;
using ReactiveUI;

namespace Threats.ViewModels.Pages;

public class StartPageViewModel : ViewModelBase
{
    public StartPageViewModel()
    {
        StartSurvey = ReactiveCommand.Create(() => { });
    }

    public IObservable<Unit> StartSurvey { get; }

    public string Version
    {
        get
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            if (version == null)
                return string.Empty;

            return $"v{version}";
        }
    }
}
