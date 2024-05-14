namespace Threats.Parser;

public interface IParser
{
    public void Init(ParsedData data);
    public void Parse();
}
