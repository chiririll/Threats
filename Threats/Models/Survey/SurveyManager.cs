using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class SurveyManager
{
    private readonly SurveyState state;

    private readonly Subject<bool> canSubmitCurrentStep = new();

    private readonly List<SurveyStep> steps;
    private int currentStep = 0;

    public SurveyManager()
    {
        state = new();

        steps = new()
        {
            new NegativesStep(state),
            new ThreatsStep(state),
        };
    }

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
