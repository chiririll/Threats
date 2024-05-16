using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Entities;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class IntrudersStageData : StageData, IIntrudersStageData
{
    [JsonProperty("potentials")] private readonly List<string> potentials = new();
    [JsonProperty("types")] private readonly List<string> types = new();
    [JsonProperty("type_descriptions")] private readonly List<string> typeDescriptions = new();

    public IntrudersStageData(
        string title,
        string nameFormat,
        string potentialFormat,
        string includedQuestionLabel,
        string typeQuestionLabel,
        string yesOption,
        string noOption) : base(title)
    {
        NameFormat = nameFormat;
        PotentialFormat = potentialFormat;

        IncludedQuestionLabel = includedQuestionLabel;
        TypeQuestionLabel = typeQuestionLabel;

        YesOption = yesOption;
        NoOption = noOption;
    }

    [JsonProperty("name_format")] public string NameFormat { get; }
    [JsonProperty("potential_format")] public string PotentialFormat { get; }

    [JsonProperty("included_question")] public string IncludedQuestionLabel { get; }
    [JsonProperty("type_question")] public string TypeQuestionLabel { get; }

    [JsonProperty("yes")] public string YesOption { get; }
    [JsonProperty("no")] public string NoOption { get; }

    public string GetIntruderTypeName(IntruderType type) => GetItem((int)type, types);
    public string GetIntruderTypeDescription(IntruderType type) => GetItem((int)type, typeDescriptions);

    public string GetPotentialName(IntruderPotential potential) => GetItem((int)potential, potentials);

    private static string GetItem(int index, IReadOnlyList<string> strings)
        => index >= 0 && index < strings.Count ? strings[index] : string.Empty;
}
