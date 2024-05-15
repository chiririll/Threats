using System.Collections.ObjectModel;
using System.Linq;
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

        Options = new(question.Options.Select(o => new OptionViewModel(o)));
    }

    public ReactiveCommand<Unit, Unit> Updated { get; }

    public LabelWithHelpButtonViewModel Label { get; }
    public ObservableCollection<OptionViewModel> Options { get; }
}
