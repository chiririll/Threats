using System.Collections.Generic;
using Threats.Data.Questions.Objects;

namespace Threats.Data;

public interface IQuestionsData
{
    public IReadOnlyList<ObjectsQuestionData> ObjectsQuestions { get; }
}
