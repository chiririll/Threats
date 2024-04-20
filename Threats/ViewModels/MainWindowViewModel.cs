using System;
using ReactiveUI;
using Threats.ViewModels.Pages;

namespace Threats.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase content;

    public MainWindowViewModel()
    {
        var startPage = new StartPageViewModel();

        startPage
            .StartPoll
            .Subscribe(_ => StartSurvey());

        content = startPage;
    }

    public ViewModelBase Content
    {
        get => content;
        private set => this.RaiseAndSetIfChanged(ref content, value);
    }

    public void StartSurvey()
    {
        Content = new PollPageViewModel();
    }
}
