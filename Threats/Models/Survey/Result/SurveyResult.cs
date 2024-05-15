using System.Collections.Generic;
using System.Collections.ObjectModel;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResult
{
    public SurveyResult(IEnumerable<Negative> negatives, IEnumerable<Object> objects)
    {
        Negatives = new(negatives);
        Objects = new(objects);
    }

    public ObservableCollection<Negative> Negatives { get; }
    public ObservableCollection<Object> Objects { get; }
}
