using Threats.Data;
using Threats.Models.Survey.Data;

namespace Threats.Models.Survey;

public abstract class SurveyStage<TState, TData> : SurveyStage
    where TState : class, IStageState
    where TData : class, IStageData
{
    protected readonly TState state;
    protected readonly TData data;

    public SurveyStage(TState state, TData data, IEntitiesData entities) : base(entities)
    {
        this.state = state;
        this.data = data;
    }

    public override string Title => data.Title;
    public TData Data => data;
}

public abstract class SurveyStage
{
    protected readonly IEntitiesData entities;

    public SurveyStage(IEntitiesData entities)
    {
        this.entities = entities;
    }

    public abstract string Title { get; }
    public abstract StageType Type { get; }

    public abstract void Init();

    public abstract void Save();

    public abstract bool CanMoveNext();

    public abstract bool CanMoveBack();

    public abstract bool MoveNext();

    public abstract bool MoveBack();
}
