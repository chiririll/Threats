using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResult
{
    public SurveyResult(IEntitiesData entities, IEnumerable<Negative> negatives, IEnumerable<Object> objects, IEnumerable<Intruder> intruders)
    {
        Negatives = new(negatives);
        Objects = new(objects);
        Intruders = new(intruders);

        var threats = entities.Threats
            .Where(t => t.ObjectIds.Any(id => objects.Any(o => o.Id == id))
            && t.Intruders.Any(i => intruders.Any(si => si.Equals(i))));
        Threats = new(threats);
    }

    public ObservableCollection<Negative> Negatives { get; }
    public ObservableCollection<Object> Objects { get; }
    public ObservableCollection<Intruder> Intruders { get; }

    public ObservableCollection<Threat> Threats { get; }
}
