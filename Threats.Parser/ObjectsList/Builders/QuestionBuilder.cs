using System.Collections.Generic;
using System.Linq;
using Threats.Data.Questions;

namespace Threats.Parser.ObjectsList;

public class QuestionBuilder
{
    private readonly int id;
    private string title = string.Empty;

    private string? helpText;
    private int helpTextIndex = -1;

    public QuestionBuilder(int id)
    {
        this.id = id;
    }

    private readonly List<OptionBuilder> options = new();

    public void AddTitle(string? title, string? separator = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(this.title))
        {
            this.title += separator ?? "\n";
        }

        this.title += title;
    }

    public void AddHelpText(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        if (helpText != null)
        {
            options[helpTextIndex].SetHelpText(helpText);
        }

        helpText = text;
        helpTextIndex = options.Count;
    }

    public OptionBuilder AddOption(string title)
    {
        var option = new OptionBuilder(options.Count + 1, id).WithTitle(title);
        options.Add(option);

        if (helpTextIndex > 0 && helpTextIndex == options.Count - 1 && helpText != null)
        {
            option.SetHelpText(helpText);
            helpText = null;
        }

        return option;
    }

    public OptionBuilder AddOptionObject(int id)
    {
        return options[^1].AddObject(id);
    }

    public QuestionData Build()
    {
        var options = this.options.Select(o => o.Build());

        return new(id, title, helpText, options);
    }
}
