using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Threats.Data.Entities;

namespace Threats.Parser.ScriptsList;

public class ScriptBuilder
{
    private const string ExampleLine = "Пример";
    private const string NoteLine = "Примечание";

    private LineType lastLineType;

    public ScriptBuilder(ScriptId id, string description)
    {
        Id = id;
        Description = description;
    }

    public ScriptId Id { get; }
    public string Description { get; private set; }
    public string Note { get; private set; } = string.Empty;
    public string Examples { get; private set; } = string.Empty;

    public Script Build() => new(Id, FixSpaces(Description!), BuildNote(), BuildExamples());

    public void AddLine(string line)
    {
        if (line.StartsWith(ExampleLine))
        {
            lastLineType = LineType.Example;
        }

        if (line.StartsWith(NoteLine))
        {
            lastLineType = LineType.Note;
        }

        switch (lastLineType)
        {
            case LineType.Description:
                Description += ' ' + line;
                break;
            case LineType.Example:
                Examples += line + ' ';
                break;
            case LineType.Note:
                Note += line + ' ';
                break;
        }
    }

    private IEnumerable<string> BuildExamples()
    {
        if (string.IsNullOrWhiteSpace(Examples))
        {
            return new List<string>();
        }

        var parts = Examples.Split(':', 2);
        if (parts.Length < 2)
        {
            Trace.TraceError($"Failed to parse examples: '{Examples}'");
            return new List<string>();
        }

        var splitRegex = new Regex(@" [0-9]+\) ");

        var examples = splitRegex.Split(parts[1]);

        return examples.Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => FixSpaces(s))
            .Select(s => s.Length > 1 ? char.ToUpperInvariant(s[0]) + s[1..] : s)
            .Select(s => s.EndsWith(';') ? s[..^1] : s);
    }

    private string? BuildNote()
    {
        if (string.IsNullOrWhiteSpace(Note))
        {
            return null;
        }

        var parts = Note.Split(':', 2);
        if (parts.Length < 2)
        {
            Trace.TraceError($"Failed to parse script note: '{Note}'");
            return null;
        }

        var note = FixSpaces(parts[1]);
        return note.Length > 1 ? char.ToUpperInvariant(note[0]) + note[1..] : note;
    }

    private string FixSpaces(string str)
    {
        var spaceRegex = new Regex(@"[ ]+");
        return spaceRegex.Replace(str, " ").Trim();
    }

    private enum LineType
    {
        Description,
        Example,
        Note,
    }
}
