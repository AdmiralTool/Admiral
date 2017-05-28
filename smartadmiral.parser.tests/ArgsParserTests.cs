using System.Collections.Generic;
using FluentAssertions;
using smartadmiral.common.Tasks;
using Xunit;

namespace smartadmiral.parser.tests
{
    public class ArgsParserTests
    {
        [Fact]
        public void Parse_HandlesSpaces()
        {
            var input = "name=name arg1='u wot m8' arg2='another fella with spaces'";

            var result = ArgsParser.Parse(input);
            var expectedResult = new Dictionary<string,string>
            {
                {"name", "name"},
                {"arg1", "'u wot m8'"},
                {"arg2", "'another fella with spaces'"},
            };

            result.ShouldBeEquivalentTo(expectedResult);
        }
    }
}