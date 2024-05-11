using System.Collections.Generic;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class SurveyManager
{
    private readonly SurveyState state;
    private readonly ISurveyData data;

    private readonly List<SurveyStep> steps;
    private int currentStep = 0;

    public SurveyManager(ISurveyData data)
    {
        this.state = new();
        this.data = data;

        steps = new()
        {
            new NegativesStep(state, data.NegativesStepData),
            new ThreatsStep(state, data.ThreatsStepData),
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

        // Переход внутри шага 
        if (current.MoveNext())
        {
            return false;
        }

        // current.Complete();
        currentStep++;

        return true;
    }
}
