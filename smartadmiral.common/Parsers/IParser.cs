using smartadmiral.common.Directives;

namespace smartadmiral.common.Parsers
{
    public interface IParser
    {
        Directive Parse(string shipsFileName, string directiveFileName);
    }
}