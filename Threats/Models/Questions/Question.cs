using System.Collections.Generic;

namespace Threats.Models.Questions;

public class Question(string description, IEnumerable<Option> options)
{
    public string Description { get; } = description;
    public IEnumerable<Option> Options { get; } = options;
}
