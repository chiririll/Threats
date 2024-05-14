using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Data.Questions;

namespace Threats.Parser;

public class ParsedData
{
    internal readonly List<Threat> threats = new();
    internal readonly List<Object> objects = new();

    internal readonly List<NegativeType> negativeTypes = new();
    internal readonly List<Negative> negatives = new();

    internal readonly List<QuestionData> objectsQuestion = new();

    public EntitiesData ToEntitiesData() => new(threats, objects, negativeTypes, negatives);
    public QuestionsData ToQuestionsData() => new(objectsQuestion);
}
