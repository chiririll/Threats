using System.Collections.Generic;

namespace Threats.Models.Questions
{
    public class Question(string name, IEnumerable<Option> options)
    {
        public string Name { get; } = name;
        public IEnumerable<Option> Options { get; } = options;
    }
}
