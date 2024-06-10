using Threats.Data.Entities;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class FocusedThreatViewModel : ViewModelBase
{
    public FocusedThreatViewModel(ThreatSelector selector)
    {
        Selector = selector;
    }

    public ThreatSelector Selector { get; }

    public Threat Threat => Selector.Threat;
}
