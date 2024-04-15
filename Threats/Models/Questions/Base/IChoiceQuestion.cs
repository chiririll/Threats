using System.Collections.Generic;

namespace Threats.Models.Questions
{
    public interface IChoiceQuestion : IQuestion
    {
        public IEnumerable<Option> Options { get; }
    }
}
