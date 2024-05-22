using Threats.Data.Entities;

namespace Threats.Models.Survey.Data;

public interface IIntrudersStageData : IStageData
{
    public string NameFormat { get; }
    public string PotentialFormat { get; }

    public string IncludedQuestionLabel { get; }
    public string TypeQuestionLabel { get; }

    public string Question1 { get; }
    public string Question2 { get; }

    public string YesOption { get; }
    public string NoOption { get; }

    public string GoalsText { get; }

    public string GetIntruderTypeName(IntruderType type);
    public string GetIntruderTypeDescription(IntruderType type);

    public string GetPotentialName(IntruderPotential potential);
    public string GetPotentialDescription(IntruderPotential potential);
}
