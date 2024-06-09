using System;
using System.Reactive;
using ReactiveUI;

namespace Threats.ViewModels.Questions;

public class YesNoOptionContainer<TPayload>
    where TPayload : class
{
    private readonly TPayload payload;

    private readonly Action<TPayload, bool, bool> converter;

    private bool option1 = false;
    private bool option2 = false;

    public YesNoOptionContainer(
            int index,
            TPayload payload,
            Action<TPayload, bool, bool> converter,
            string label) : this(index, payload, converter, new LabelWithHelpButtonViewModel(new(label)))
    {
    }

    public YesNoOptionContainer(
        int index,
        TPayload payload,
        Action<TPayload, bool, bool> converter,
        LabelWithHelpButtonViewModel label)
    {
        this.payload = payload;
        this.converter = converter;

        Index = index + 1;
        Label = label;
        Group = $"q{Index}";

        OptionUpdated = ReactiveCommand.Create(() => { });
    }

    public int Index { get; }
    public string Group { get; }

    public LabelWithHelpButtonViewModel Label { get; }

    public ReactiveCommand<Unit, Unit> OptionUpdated { get; }

    public bool Option1
    {
        get => option1;
        set
        {
            option1 = value;
            Convert();
        }
    }

    public bool Option2
    {
        get => option2;
        set
        {
            option2 = value;
            Convert();
        }
    }

    public bool IsEven => Index % 2 == 0;
    public bool IsOdd => Index % 2 != 0;

    private void Convert() => converter.Invoke(payload, option1, option2);
}
