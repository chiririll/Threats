using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Survey;

namespace Threats.ViewModels.Survey;

public class FocusedThreatViewModel : ViewModelBase
{
    private readonly ThreatSelector selector;
    private readonly IEntitiesData entities;

    public FocusedThreatViewModel(ThreatSelector selector, IEntitiesData entities)
    {
        this.selector = selector;
        this.entities = entities;

        var scriptTypes = selector.Threat.ScriptIds
            .GroupBy(s => s.Type)
            .Select(g => new Tuple<ScriptType?, IEnumerable<ScriptId>>(entities.GetScriptTypeById(g.Key), g))
            .Where(g => g.Item1 != null)
            .Select(g => new ScriptTypeViewModel(g.Item1!, g.Item2.Select(id => entities.GetScriptById(id)).Where(s => s != null).Cast<Script>()));

        ScriptTypes = new(scriptTypes);
    }

    public Threat Threat => selector.Threat;

    public ObservableCollection<ScriptTypeViewModel> ScriptTypes { get; }
}
