using System.Collections.Generic;
using System.Linq;
using Threats.Data.Entities;
using Threats.Models.Survey.Stages.Intruders;

namespace Threats.Models.Survey;

public class IntrudersStageState : IStageState
{
    private readonly Dictionary<int, IntruderBuilder> selectedIntruders = new();

    private List<Intruder>? intruders;
    private bool altered = false;

    public IReadOnlyDictionary<int, IntruderBuilder> SelectedIntruders => selectedIntruders;

    public void SelectIntruder(IntruderData data, bool selected)
    {
        altered = true;
        if (selected)
        {
            selectedIntruders[data.Id] = new(data);
        }
        else
        {
            selectedIntruders.Remove(data.Id);
        }
    }

    public IReadOnlyList<Intruder> GetIntruders()
    {
        if (intruders == null || altered)
        {
            intruders = selectedIntruders.Values.Select(i => i.Build()).ToList();
            altered = false;
        }

        return intruders;
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithIntruders(GetIntruders());
    }
}
