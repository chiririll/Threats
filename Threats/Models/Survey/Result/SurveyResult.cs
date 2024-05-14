using System.Collections.Generic;
using System.Collections.ObjectModel;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class SurveyResult
{
    public SurveyResult(IEnumerable<Negative> negatives)
    {
        Negatives = new(negatives);
    }

    public ObservableCollection<Negative> Negatives { get; }
}
