using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using Threats.Models.Questions;
using Threats.ViewModels.Poll;

namespace Threats.ViewModels.Pages;

public class PollPageViewModel : ViewModelBase
{
    private QuestionViewModel question;

    public PollPageViewModel()
    {
        var q = new Question("Test question:",
        [
            new (1, "Test 1"),
            new (2, "Test 2"),
        ]);

        question = new(q);
    }

    public QuestionViewModel Question
    {
        get => question;
        private set => this.RaiseAndSetIfChanged(ref question, value);
    }
}
