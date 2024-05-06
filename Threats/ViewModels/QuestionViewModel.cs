using System.Collections.ObjectModel;
using ReactiveUI;
using Threats.Models.Questions;

namespace Threats.ViewModels.Survey;

public class QuestionViewModel : ViewModelBase
{
    private string description;

    public QuestionViewModel(Question question)
    {
        description = question.Description;
        Options = new(question.Options);
    }

    public string Description
    {
        get => description;
        private set => this.RaiseAndSetIfChanged(ref description, value);
    }

    public ObservableCollection<Option> Options { get; }
}
