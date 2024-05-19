using System.Collections.Generic;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;
using Threats.Models.Questions;
using Threats.Models.Survey.Data;
using Threats.Models.Survey.State;

namespace Threats.Models.Survey;

public class IntrudersStage : SurveyStage<IntrudersStageState, IIntrudersStageData>
{
    private int currentIndex = 0;

    public IntrudersStage(
        SurveyState state,
        IIntrudersStageData data,
        IEntitiesData entities) : base(state.IntrudersStage, data, entities)
    {
        IntruderIncluded = CreateIncludedQuestion();
        IntruderType = CreateTypeQuestion();
    }

    public IntruderData? CurrentIntruder => currentIndex >= 0 && currentIndex < entities.Intruders.Count
        ? entities.Intruders[currentIndex]
        : null;

    public Question IntruderIncluded { get; private set; }
    public Question IntruderType { get; private set; }

    public override void Save()
    {
        if (IntruderIncluded.SelectedOne == null || IntruderIncluded.SelectedOne.Id != OptionId.Yes)
        {
            return;
        }

        if (IntruderType.SelectedOne?.Payload is not IntruderTypePayload payload)
        {
            return;
        }

        state.AddIntruder(new(payload.Type, CurrentIntruder!.Potential));
    }

    public override bool CanMoveNext()
    {
        var selected = IntruderIncluded.SelectedOne;
        if (selected == null)
        {
            return false;
        }

        if (selected.Id == OptionId.No)
        {
            return true;
        }

        return IntruderType.SelectedOne != null;
    }

    public override bool MoveNext()
    {
        currentIndex++;
        if (CurrentIntruder == null)
        {
            return false;
        }

        IntruderIncluded = CreateIncludedQuestion();
        IntruderType = CreateTypeQuestion();

        return true;
    }

    private Question CreateIncludedQuestion() => new(data.IncludedQuestionLabel, new List<Option>
    {
        new(OptionId.Yes, data.YesOption, OptionId.IncludedGroup),
        new(OptionId.No, data.NoOption, OptionId.IncludedGroup),
    });

    private Question CreateTypeQuestion() => new(
        data.TypeQuestionLabel,
        System.Enum.GetValues(typeof(IntruderType))
            .Cast<IntruderType>()
            .Skip(1)
            .Select((t, i) => new Option(
                i + 1,
                data.GetIntruderTypeName(t),
                OptionId.TypeGroup,
                data.GetIntruderTypeDescription(t),
                new IntruderTypePayload(t))));

    private static class OptionId
    {
        public const int Yes = 1;
        public const int No = 2;

        public const string IncludedGroup = "inc";
        public const string TypeGroup = "type";
    }
}
