using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Questions.Objects;

namespace Threats.Data;

public class QuestionsData : IQuestionsData
{
    [JsonProperty("objects")] private readonly List<ObjectsQuestionData> objectsQuestions = new();

    public QuestionsData(IEnumerable<ObjectsQuestionData> objectsQuestions)
    {
        this.objectsQuestions.AddRange(objectsQuestions);
    }

    [JsonIgnore] public IReadOnlyList<ObjectsQuestionData> ObjectsQuestions => objectsQuestions;

    public static QuestionsData FromJson(string json)
    {
        return JsonConvert.DeserializeObject<QuestionsData>(json)!;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
