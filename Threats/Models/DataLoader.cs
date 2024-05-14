using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Threats.Models.Survey.Data;

namespace Threats.Data;

internal static class DataLoader
{
    private const string entitiesResource = "entities.json";
    private const string questionsResource = "questions.json";
    private const string surveyDataResource = "stages.json";

    private static readonly Assembly assembly;
    private static readonly string assemblyName;

    static DataLoader()
    {
        assembly = Assembly.GetAssembly(typeof(DataLoader))!;
        assemblyName = assembly.GetName().Name!;
    }

    internal static EntitiesData LoadEntitiesData()
    {
        var json = GetResourceJson(entitiesResource);

        return EntitiesData.FromJson(json);
    }

    internal static QuestionsData LoadQuestionsData()
    {
        var json = GetResourceJson(questionsResource);

        return QuestionsData.FromJson(json);
    }

    internal static SurveyData LoadSurveyData()
    {
        var json = GetResourceJson(surveyDataResource);

        return JsonConvert.DeserializeObject<SurveyData>(json)!;
    }

    private static Stream GetResource(string name) => assembly.GetManifestResourceStream($"{assemblyName}.{name}")!;

    private static string GetResourceJson(string name)
    {
        var stream = GetResource(name);
        var reader = new StreamReader(stream, Encoding.UTF8);

        var json = reader.ReadToEnd();

        reader.Close();
        stream.Close();

        return json;
    }
}
