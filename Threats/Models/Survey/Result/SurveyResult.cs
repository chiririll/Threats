using System.Collections.Generic;
using System.Collections.ObjectModel;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResult
{
    public SurveyResult(
        IEnumerable<Negative> negatives,
        IEnumerable<Object> objects,
        IEnumerable<Intruder> intruders,
        IEnumerable<Threat> threats)
    {
        Negatives = new(negatives);
        Objects = new(objects);
        Intruders = new(intruders);
        Threats = new(threats);
    }

    public ObservableCollection<Negative> Negatives { get; }
    public ObservableCollection<Object> Objects { get; }
    public ObservableCollection<Intruder> Intruders { get; }
    public ObservableCollection<Threat> Threats { get; }
}
