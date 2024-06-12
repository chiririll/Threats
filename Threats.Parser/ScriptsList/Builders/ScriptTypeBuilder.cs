using System.Diagnostics;
using System.Text.RegularExpressions;
using Threats.Data.Entities;

namespace Threats.Parser.ScriptsList;

public class ScriptTypeBuilder
{
    private const string TaskLine = "Тактическая задача";
    private const string NoteLine = "Примечание";

    private LineType lastLineType;

    public ScriptTypeBuilder(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public string Name { get; set; } = string.Empty;
    public string Task { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;

    public ScriptType Build() => new(Id, FixSpaces(Name), FixLine(Task)!, FixLine(Note));

    public void AddLine(string? line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return;
        }

        if (line.StartsWith(TaskLine))
        {
            lastLineType = LineType.Task;
        }
        else if (line.StartsWith(NoteLine))
        {
            lastLineType = LineType.Note;

        }

        switch (lastLineType)
        {
            case LineType.Name:
                Name += line + ' ';
                break;
            case LineType.Task:
                Task += line + ' ';
                break;
            case LineType.Note:
                Note += line + ' ';
                break;
        }
    }

    private string? FixLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return null;
        }

        var parts = line.Split(':', 2);
        if (parts.Length < 2)
        {
            Trace.TraceError($"Failed to parse line: '{line}'");
            return null;
        }

        var l = FixSpaces(parts[1]);
        return l.Length > 1 ? char.ToUpperInvariant(l[0]) + l[1..] : l;
    }

    private string FixSpaces(string str)
    {
        var spaceRegex = new Regex(@"[ ]+");
        return spaceRegex.Replace(str, " ").Trim();
    }

    private enum LineType
    {
        Name,
        Task,
        Note,
    }
}
