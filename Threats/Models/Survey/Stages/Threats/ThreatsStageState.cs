using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStageState : IStageState
{
    private readonly List<ThreatSelector> selectors = new();
    private IEnumerable<Threat>? threats;

    public IReadOnlyList<ThreatSelector> Selectors => selectors;

    public void Init(SurveyState state, IEntitiesData entities)
    {
        var threats = entities.Threats
            .Where(t => t.ObjectIds.Any(id => state.ObjectsStage.Objects.Any(o => o.Id == id))
            && t.Intruders.Any(i => state.IntrudersStage.GetIntruders().Any(si => si.Equals(i))));

        selectors.Clear();
        selectors.AddRange(threats.Select(t => new ThreatSelector(t, entities)));
    }

    public void BuildThreats()
    {
        threats = selectors
            .Where(s => s.Selected)
            .Select(s => s.Threat);
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithThreats(threats!);
    }
}
