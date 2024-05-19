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
    private readonly IQuestionsData questions;

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
            new NegativesStage(state, data.NegativesStageData, entities),
            new ObjectsStage(state, data.ObjectsStageData, entities, questions),
            new ObjectsAppendStage(state,data.ObjectsStageData, entities),
            new IntrudersStage(state, data.IntrudersStageData, entities),
        };
    }

    public string Title => CurrentStage != null
        ? string.Format(data.TitleFormat, currentStage + 1, CurrentStage.Title)
        : string.Empty;

    public SurveyStage? CurrentStage => currentStage >= 0 && currentStage < stages.Count
        ? stages[currentStage]
        : null;

    public bool CanMoveNext() => CurrentStage?.CanMoveNext() ?? false;

    public bool MoveToNextStage()
    {
        var current = CurrentStage;
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

        currentStage++;

        return CurrentStage != null;
    }

    public SurveyResult GetResult() => state.GetResult(entities);
}
