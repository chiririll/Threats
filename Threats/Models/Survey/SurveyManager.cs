using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class SurveyManager
{
    private readonly SurveyState state;
    private readonly ISurveyData data;

    private readonly Subject<bool> canSubmitCurrentStep = new();

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

    public IObservable<bool> CanSubmitCurrentStep => canSubmitCurrentStep;

    public bool MoveToNextStep()
    {
        var current = CurrentStep;
        if (current == null)
        {
            return false;
        }

        // current.Complete();
        currentStep++;

        return true;
    }
}
