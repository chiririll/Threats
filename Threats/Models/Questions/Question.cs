using System.Collections.Generic;

namespace Threats.Models.Questions;

public class Question
{
    public Question(string description, IEnumerable<Option> options)
    {
        Description = description;
        Options = options;
    }

    public string Description { get; }
    public IEnumerable<Option> Options { get; }
}
