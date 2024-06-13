using System.Linq;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class ThreatSelector
{
    public ThreatSelector(Threat threat)
    {
        Threat = threat;

        var scripts = threat.ScriptIds
            .Select(s => s.ToString());
    }

    public Threat Threat { get; }
    public bool Selected { get; set; }
}
