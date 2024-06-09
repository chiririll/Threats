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

    /// <summary>
    /// Проверка текущего состояния этапа на готовность перехода к следующему состоянию или этапу
    /// </summary>
    public abstract bool CanMoveNext();

    /// <summary>
    /// Проверка текущего состояния этапа на возможность перехода к предыдущему состоянию или этапу
    /// </summary>
    public abstract bool CanMoveBack();

    /// <summary>
    /// Совершить попытку перевода этапа в следующее состояние
    /// </summary>
    /// <returns>true - Переход обработан внутри этапа; false - Состояний больше нет, необходим переход к следующему этапу</returns>
    public abstract bool MoveNext();

    /// <summary>
    /// Совершить попытку перевода этапа в предыдущее состояние
    /// </summary>
    /// <returns>true - Переход обработан внутри этапа; false - Состояния нет, необходим переход к предыдущему этапу</returns>
    public abstract bool MoveBack();
}
