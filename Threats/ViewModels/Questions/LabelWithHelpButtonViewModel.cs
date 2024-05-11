using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class LabelWithHelpButtonViewModel : ViewModelBase
{
    private readonly LabelWithDescription label;

    public LabelWithHelpButtonViewModel(LabelWithDescription label)
    {
        this.label = label;
    }

    public string Label => label.Label;
    public string Description => label.Description;
    public bool HasDescription => label.HasDescription;
}
