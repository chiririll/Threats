using Threats.Models.Questions;

namespace Threats.Models.Poll
{
    public abstract class Selector
    {
        public abstract Question GetQuestion();
        public abstract void SetResult(Question result);
    }
}
