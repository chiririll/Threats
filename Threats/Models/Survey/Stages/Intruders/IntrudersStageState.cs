using System.Collections.Generic;
using Threats.Data.Entities;
using Threats.Models.Survey.Stages.Intruders;

namespace Threats.Models.Survey;

public class IntrudersStageState : IStageState
{
    private readonly HashSet<IntruderBuilder> selectedIntruders = new();
    private readonly HashSet<Intruder> intruders = new();

    public IReadOnlySet<IntruderBuilder> SelectedIntruders => selectedIntruders;

    public void SelectIntruder(IntruderData data)
    {
        selectedIntruders.Add(new(data));
    }

    public void BuildIntruders()
    {
        foreach (var builder in selectedIntruders)
        {
            intruders.Add(builder.Build());
        }

        selectedIntruders.Clear();
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithIntruders(intruders);
    }
}
