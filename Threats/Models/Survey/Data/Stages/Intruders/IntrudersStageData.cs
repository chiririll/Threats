using System.Collections.Generic;
using Newtonsoft.Json;
using Threats.Data.Entities;
using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class IntrudersStageData : StageData, IIntrudersStageData
{
    [JsonProperty("potentials")] private readonly List<string> potentials = new();
    [JsonProperty("potential_descriptions")] private readonly List<string> potentialDescriptions = new();
    [JsonProperty("types")] private readonly List<string> types = new();
    [JsonProperty("type_descriptions")] private readonly List<string> typeDescriptions = new();

    [JsonProperty("name_format")] public string NameFormat { get; private set; } = string.Empty;
    [JsonProperty("potential_format")] public string PotentialFormat { get; private set; } = string.Empty;

    [JsonProperty("included_question")] public string IncludedQuestionLabel { get; private set; } = string.Empty;
    [JsonProperty("type_question")] public string TypeQuestionLabel { get; private set; } = string.Empty;

    [JsonProperty("question_1")] public string Question1 { get; private set; } = string.Empty;
    [JsonProperty("question_2")] public string Question2 { get; private set; } = string.Empty;

    [JsonProperty("yes")] public string YesOption { get; private set; } = string.Empty;
    [JsonProperty("no")] public string NoOption { get; private set; } = string.Empty;

    [JsonProperty("goals")] public string GoalsText { get; private set; } = string.Empty;

    public string GetIntruderTypeName(IntruderType type) => GetItem((int)type, types);
    public string GetIntruderTypeDescription(IntruderType type) => GetItem((int)type, typeDescriptions);

    public string GetPotentialName(IntruderPotential potential) => GetItem((int)potential, potentials);
    public string GetPotentialDescription(IntruderPotential potential) => GetItem((int)potential, potentialDescriptions);

    private static string GetItem(int index, IReadOnlyList<string> strings)
        => index >= 0 && index < strings.Count ? strings[index] : string.Empty;
}
