using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class QuestionViewModel : ViewModelBase
{
    public QuestionViewModel(Question question)
    {
        Label = new(question.Label);

        Updated = ReactiveCommand.Create(() => { });

        Options = new(question.Options);
    }

    public ReactiveCommand<Unit, Unit> Updated { get; }

    public LabelWithHelpButtonViewModel Label { get; }
    public ObservableCollection<Option> Options { get; }
}
