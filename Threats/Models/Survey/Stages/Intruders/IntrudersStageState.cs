using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey;

public class IntrudersStageState : IStageState
{
    private readonly HashSet<int> selectedIntruders = new();
    private readonly HashSet<Intruder> intruders = new();

    public void SelectIntruder(int id)
    {
        selectedIntruders.Add(id);
    }

    public void AddIntruder(Intruder intruder)
    {
        intruders.Add(intruder);
    }

    public void FillResult(SurveyResultBuilder builder)
    {
        builder.WithIntruders(intruders);
    }
}
