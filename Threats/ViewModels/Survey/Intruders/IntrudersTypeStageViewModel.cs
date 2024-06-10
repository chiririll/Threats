using System;
using System.Collections.ObjectModel;
using System.Linq;
using Threats.Data.Entities;
using Threats.Models.Survey;
using Threats.Models.Survey.Stages.Intruders;
using Threats.ViewModels.Questions;

namespace Threats.ViewModels.Survey;

public class IntrudersTypeStageViewModel : SurveyStageViewModel<IntrudersTypeStage>
{
    public IntrudersTypeStageViewModel(IntrudersTypeStage stage) : base(stage)
    {
        Intruders = new(stage.Intruders.Select((b, i) =>
        {
            var vm = new YesNoOptionContainer<IntruderBuilder>(
                i, b,
                (intruder, @internal, @external) => intruder.SetType(@internal && !@external
                    ? IntruderType.Internal
                    : !@internal && external ? IntruderType.External : IntruderType.None),
                $"{i + 1}. {b.Data.Title}");

            vm.OptionUpdated.Subscribe(_ => updated.OnNext(default));

            return vm;
        }));

        InternalLabel = new(new(
            stage.Data.GetIntruderTypeName(IntruderType.Internal),
            stage.Data.GetIntruderTypeDescription(IntruderType.Internal)));

        ExternalLabel = new(new(
            stage.Data.GetIntruderTypeName(IntruderType.External),
            stage.Data.GetIntruderTypeDescription(IntruderType.External)));
    }

    public ObservableCollection<YesNoOptionContainer<IntruderBuilder>> Intruders { get; }
    public string TypeQuestionLabel => stage.Data.TypeQuestionLabel;

    public LabelWithHelpButtonViewModel InternalLabel { get; }
    public LabelWithHelpButtonViewModel ExternalLabel { get; }

    public override void Refresh()
    {
    }
}
