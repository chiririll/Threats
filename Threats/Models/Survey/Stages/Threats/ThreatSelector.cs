using System.Linq;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class ThreatSelector
{
    public ThreatSelector(Threat threat, IEntitiesData entities)
    {
        Threat = threat;

        var scripts = threat.ScriptIds
            .Select(id => entities.GetScriptById(id))
            .Where(s => s != null)
            .Select(s => $"Т{s!.Type}.{s!.Id}");

        Scripts = string.Join("; ", scripts);
    }

    public Threat Threat { get; }
    public string Scripts { get; }
    public bool Selected { get; set; }
}
