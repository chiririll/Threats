using System.Collections.Generic;

namespace Threats.Models.Questions
{
    public interface IMultipleChoiceQuestion : IChoiceQuestion
    {
        public IEnumerable<Option> Selected { get; }
    }
}
