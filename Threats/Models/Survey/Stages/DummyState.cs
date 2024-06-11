using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Data.Questions;

namespace Threats.Models.Survey.State;

public class DummyState
{
    public static SurveyState Generate()
    {
        var entities = DataLoader.LoadEntitiesData();
        var questions = DataLoader.LoadQuestionsData();

        return new(
            Negatives(),
            Objects(entities, questions),
            Intruders(entities),
            Threats());
    }

    private static NegativesStageState Negatives()
    {
        return new();
    }

    private static ObjectsStageState Objects(IEntitiesData entities, IQuestionsData questions)
    {
        var state = new ObjectsStageState();

        var objects = questions.ObjectsQuestions
            .SelectMany(q => q.Options
                .Select(o => o.Payload)
                .Cast<ObjectsOptionPayload>()
                .SelectMany(o => o.ObjectsToAdd))
            .Distinct()
            .Select(id => entities.GetObjectById(id)!);
        state.SetObjects(objects);

        return state;
    }

    private static IntrudersStageState Intruders(IEntitiesData entities)
    {
        var state = new IntrudersStageState();

        foreach (var intruder in entities.Intruders)
        {
            state.SelectIntruder(intruder, true);
            state.SelectedIntruders[intruder.Id].SetType(IntruderType.Internal);
        }

        return state;
    }

    private static ThreatsStageState Threats()
    {
        return new();
    }
}
