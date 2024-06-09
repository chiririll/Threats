using System.Collections.Generic;
using Threats.Data;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class ThreatsStage : SurveyStage<ThreatsStageState, IThreatsStageData>
{
    private readonly SurveyState surveyState;

    public ThreatsStage(SurveyState surveyState, IThreatsStageData data, IEntitiesData entities)
        : base(surveyState.ThreatsStage, data, entities)
    {
        this.surveyState = surveyState;
    }

    public IReadOnlyList<ThreatSelector> Selectors => state.Selectors;

    public override StageType Type => StageType.Threats;

    public override void Init()
    {
        state.Init(surveyState, entities);
    }

    public override void Save()
    {
        state.BuildThreats();
    }

    public override bool CanMoveNext() => true;
    public override bool CanMoveBack() => true;

    public override bool MoveNext() => false;
    public override bool MoveBack() => false;
}
