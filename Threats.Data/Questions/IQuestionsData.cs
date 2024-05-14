using System.Collections.Generic;
using Threats.Data.Questions;

namespace Threats.Data;

public interface IQuestionsData
{
    public IReadOnlyList<QuestionData> ObjectsQuestions { get; }
}
