using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.IntrudersList;

public class IntruderBuilder
{
    private readonly int id;

    private string? title;
    private IntruderPotential potential = IntruderPotential.None;

    private readonly List<string> goals = new();

    public IntruderBuilder(int id)
    {
        this.id = id;
    }

    public void AddTitle(string? title, string? separator = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(this.title))
        {
            this.title += separator ?? " ";
        }

        this.title += title;
    }

    public void TryAddPotential(string? potentialString)
    {
        if (potential != IntruderPotential.None || string.IsNullOrWhiteSpace(potentialString))
        {
            return;
        }

        potential = ParsePotential(potentialString);
    }

    private static IntruderPotential ParsePotential(string str)
    {
        if (str.Contains("(Н1)")) return IntruderPotential.Base;
        if (str.Contains("(Н2)")) return IntruderPotential.Advanced;
        if (str.Contains("(Н3)")) return IntruderPotential.Medium;
        if (str.Contains("(Н4)")) return IntruderPotential.High;

        return IntruderPotential.None;
    }

    public void AddGoal(string? goal)
    {
        if (string.IsNullOrWhiteSpace(goal))
        {
            return;
        }

        goal = goal.Trim();
        if (goal[^1] == '.')
        {
            goal = goal[..^1];
        }

        goals.Add(goal);
    }

    public IntruderData Build() => new(
        id,
        title?.Trim() ?? string.Empty,
        potential,
        goals);
}
