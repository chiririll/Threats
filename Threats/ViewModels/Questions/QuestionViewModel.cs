using System.Collections.ObjectModel;
using ReactiveUI;
using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class QuestionViewModel : ViewModelBase
{
    private string description;

    public QuestionViewModel(Question question)
    {
        Label = new(question.Label);

        Options = new(question.Options);
    }

    public string Description
    {
        get => description;
        private set => this.RaiseAndSetIfChanged(ref description, value);
    }

    public ObservableCollection<Option> Options { get; }
    public LabelWithHelpButtonViewModel Label { get; }
}
