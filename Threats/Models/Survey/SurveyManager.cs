using System.Collections.Generic;
using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class SurveyManager
{
    private readonly SurveyState state;
    private readonly ISurveyData data;
    private readonly IEntitiesData entities;

    private readonly List<SurveyStep> steps;
    private int currentStep = 0;

    public SurveyManager(ISurveyData data, IEntitiesData entities)
    {
        this.state = new();
        this.data = data;
        this.entities = entities;

        steps = new()
        {
            new NegativesStep(state, data.NegativesStepData, entities),
            new ThreatsStep(state, data.ThreatsStepData, entities),
        };
    }

    public string Title => CurrentStep != null
        ? string.Format(data.TitleFormat, currentStep + 1, CurrentStep.Title)
        : string.Empty;

    public SurveyStep? CurrentStep => currentStep >= 0 && currentStep < steps.Count
        ? steps[currentStep]
        : null;

    public bool CanMoveNext() => CurrentStep?.CanMoveNext() ?? false;

    public bool MoveToNextStep()
    {
        var current = CurrentStep;
        if (current == null)
        {
            return false;
        }

        current.Save();

        // Переход внутри шага 
        if (current.MoveNext())
        {
            return true;
        }

        // current.Complete();
        currentStep++;

        return true;
    }
}
