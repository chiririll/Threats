using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class QuestionViewModel : ViewModelBase
{
    private readonly Subject<Unit> updated = new();

    public QuestionViewModel(Question question, bool boldTitle = false)
    {
        Label = new(question.Label, boldTitle ? Avalonia.Media.FontWeight.SemiBold : Avalonia.Media.FontWeight.Normal);

        Options = new(question.Options.Select(o => new OptionViewModel(o)));
    }

    public IObservable<Unit> OnUpdate => updated;

    public LabelWithHelpButtonViewModel Label { get; }
    public ObservableCollection<OptionViewModel> Options { get; }

    public void Updated()
    {
        updated.OnNext(default);
    }
}
