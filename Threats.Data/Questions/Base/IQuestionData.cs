using System.Collections.Generic;

namespace Threats.Data.Questions;

public interface IQuestionData<TOptionData>
    where TOptionData : IOptionData
{

    public int Id { get; }

    public string Title { get; }
    public string? HelpText { get; }

    public IReadOnlyList<TOptionData> Options { get; }
}

public interface IQuestionData
{
    public int Id { get; }

    public string Title { get; }
    public string? HelpText { get; }
}