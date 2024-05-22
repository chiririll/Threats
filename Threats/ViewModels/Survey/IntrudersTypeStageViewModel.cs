using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
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
            var vm = new IntruderVM(i, b);
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

    public ObservableCollection<IntruderVM> Intruders { get; }
    public string TypeQuestionLabel => stage.Data.TypeQuestionLabel;

    public LabelWithHelpButtonViewModel InternalLabel { get; }
    public LabelWithHelpButtonViewModel ExternalLabel { get; }

    public override void Refresh()
    {
    }

    public class IntruderVM
    {
        private readonly IntruderBuilder builder;

        private bool @internal = false;
        private bool @external = false;

        public IntruderVM(int index, IntruderBuilder builder)
        {
            this.builder = builder;

            Index = index + 1;
            Name = $"{Index}. {builder.Data.Title}";
            Group = $"q{Index}";

            OptionUpdated = ReactiveCommand.Create(() => { });
        }

        public int Index { get; }
        public string Group { get; }
        public string Name { get; }

        public ReactiveCommand<Unit, Unit> OptionUpdated { get; }

        public bool Internal
        {
            get => @internal;
            set
            {
                @internal = value;
                UpdateType();
            }
        }

        public bool External
        {
            get => @external;
            set
            {
                @external = value;
                UpdateType();
            }
        }

        private void UpdateType()
        {
            var type = Internal && !External
                ? IntruderType.Internal
                : !Internal && External
                    ? IntruderType.External
                    : IntruderType.None;
            builder.SetType(type);
        }
    }
}
