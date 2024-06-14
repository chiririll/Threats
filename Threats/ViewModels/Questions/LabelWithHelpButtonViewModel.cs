using Avalonia.Media;
using Threats.Models.Questions;

namespace Threats.ViewModels.Questions;

public class LabelWithHelpButtonViewModel : ViewModelBase
{
    private readonly LabelWithDescription label;

    public LabelWithHelpButtonViewModel(LabelWithDescription label, FontWeight fontWeight = FontWeight.Normal)
    {
        this.label = label;
        FontWeight = fontWeight;
    }

    public string Label => label.Label;
    public FontWeight FontWeight { get; }
    public string Description => label.Description;
    public bool HasDescription => label.HasDescription;
}
