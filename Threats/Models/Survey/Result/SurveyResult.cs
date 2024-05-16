using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Data.Questions;

namespace Threats.Models.Survey;

public class SurveyResult
{
    public SurveyResult(IEntitiesData entities, IEnumerable<Negative> negatives, IEnumerable<Object> objects, IEnumerable<Intruder> intruders)
    {
        Negatives = new(negatives);
        Objects = new(objects);
        Intruders = new(intruders);

        var threats = entities.Threats
            .Where(t => t.ObjectIds.Any(id => objects.Any(o => o.Id == id))
            && t.Intruders.Any(i => intruders.Any(si => si.Equals(i))));
        Threats = new(threats);
    }

    public ObservableCollection<Negative> Negatives { get; }
    public ObservableCollection<Object> Objects { get; }
    public ObservableCollection<Intruder> Intruders { get; }

    public ObservableCollection<Threat> Threats { get; }

    public static SurveyResult Test(IEntitiesData entities, IQuestionsData questions)
    {
        var objects = questions.ObjectsQuestions
            .SelectMany(q => q.Options
                .Select(o => o.Payload)
                .Cast<ObjectsOptionPayload>()
                .SelectMany(o => o.ObjectsToAdd))
            .Distinct()
            .Select(id => entities.GetObjectById(id)!);

        var intruders = new List<Intruder>()
        {
            new (IntruderType.Internal, IntruderPotential.Base),
            new (IntruderType.Internal, IntruderPotential.Advanced),
            new (IntruderType.Internal, IntruderPotential.Medium),
            new (IntruderType.Internal, IntruderPotential.High),
            new (IntruderType.External, IntruderPotential.Base),
            new (IntruderType.External, IntruderPotential.Advanced),
            new (IntruderType.External, IntruderPotential.Medium),
            new (IntruderType.External, IntruderPotential.High),
        };

        return new(entities, new List<Negative>(), objects, intruders);
    }
}
