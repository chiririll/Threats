using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Questions;

namespace Threats.Data;

public class QuestionsData
{
    [JsonProperty("objects")] private readonly List<QuestionData> objectsQuestions = new();

    public QuestionsData(IEnumerable<QuestionData> objectsQuestions)
    {
        this.objectsQuestions.AddRange(objectsQuestions);
    }

    [JsonIgnore] public IReadOnlyList<QuestionData> ObjectsQuestions => objectsQuestions;

    public static QuestionsData FromJson(string json)
    {
        return JsonConvert.DeserializeObject<QuestionsData>(json)!;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
