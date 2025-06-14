using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class SurveyManager
{
    private readonly SurveyState state;
    private readonly ISurveyData data;

    private readonly IEntitiesData entities;
    private readonly IQuestionsData questions;

    private readonly Subject<Unit> onStageChanged = new();

    private readonly List<SurveyStage> stages;
    private int currentStage = 0;

    public SurveyManager(ISurveyData data, IEntitiesData entities, IQuestionsData questions)
    {
        this.state = new();
        this.data = data;

        this.entities = entities;
        this.questions = questions;

        stages = new()
        {
            new NegativeTypesStage(state, data.NegativesStageData, entities),
            new NegativesStage(state, data.NegativesStageData, entities),
            new ObjectsStage(this, state, data.ObjectsStageData, entities, questions),
            new ObjectsAppendStage(state,data.ObjectsStageData, entities),
            new IntrudersStage(state, data.IntrudersStageData, entities),
            new IntrudersTypeStage(state,data.IntrudersStageData, entities),
            new ThreatsStage(state, data.ThreatsStageData, entities),
        };

        stages[0].Init();
    }

    public IObservable<Unit> OnStateChanged => onStageChanged;

    public string Title => CurrentStage != null
        ? string.Format(data.TitleFormat, (int)CurrentStage.Type, CurrentStage.Title)
        : string.Empty;

    public SurveyStage? CurrentStage => currentStage >= 0 && currentStage < stages.Count
        ? stages[currentStage]
        : null;

    public string? ActionName => CurrentStage?.ActionName;

    public bool CanMoveBack() => currentStage > 0 || (CurrentStage?.CanMoveBack() ?? false);
    public bool CanMoveNext() => CurrentStage?.CanMoveNext() ?? false;

    public bool MoveToNextStage()
    {
        if (!CanMoveNext())
        {
            return false;
        }

        var current = CurrentStage;
        if (current == null)
        {
            return false;
        }

        current.Save();

        // Переход внутри этапа
        if (current.MoveNext())
        {
            onStageChanged.OnNext(default);
            return true;
        }

        currentStage++;

        if (CurrentStage == null)
        {
            return false;
        }

        CurrentStage.Init();

        onStageChanged.OnNext(default);
        return true;
    }

    public bool MoveToPreviousStage()
    {
        if (!CanMoveBack())
        {
            return false;
        }

        var current = CurrentStage;
        if (current == null)
        {
            return false;
        }

        current.Save();

        // Переход внутри этапа
        if (current.MoveBack())
        {
            onStageChanged.OnNext(default);
            return true;
        }

        currentStage--;
        CurrentStage!.Init();

        onStageChanged.OnNext(default);
        return true;
    }

    public void InvokeAction() => CurrentStage?.InvokeAction();

    public SurveyResult GetResult() => state.GetResult();
}
