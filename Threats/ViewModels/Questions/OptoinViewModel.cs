using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class OptionViewModel
{
    private readonly Option option;

    public OptionViewModel(Option option)
    {
        this.option = option;

        Label = new(option.Label);
    }

    public LabelWithHelpButtonViewModel Label { get; }

    public bool Selected
    {
        get => option.Selected;
        set => option.Selected = value;
    }

    public string Group => option.Group;
}
