using Threats.Models.Survey.Data;

namespace Threats.Models.Survey;

public abstract class SurveyStep<TState, TData> : SurveyStep
    where TData : IStepData
{
    protected readonly TState state;
    protected readonly TData data;

    public SurveyStep(TState state, TData data)
    {
        this.state = state;
        this.data = data;
    }

    public override string Title => data.Title;
}

public abstract class SurveyStep
{
    public SurveyStep()
    {
    }

    public abstract string Title { get; }

    public abstract void Save();

    /// <summary>
    /// Проверка текущего состояния этапа на готовность перехода к следующему состоянию или этапу
    /// </summary>
    public abstract bool CanMoveNext();

    /// <summary>
    /// Совершить попытку перевода этапа в следующее состояние
    /// </summary>
    /// <returns>true - Переход обработан внутри этапа; false - Состояний больше нет, необходим переход к следующему этапу</returns>
    public abstract bool MoveNext();
}
