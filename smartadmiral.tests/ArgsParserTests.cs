using FluentAssertions;
using smartadmiral.console.Args;
using Xunit;

namespace smartadmiral.tests
{
    public class ArgsParserTests
    {
        [Fact]
        public void ParseArgs_WithSimpleExample_ReturnsCorrectOutput()
        {
            var pathToShips = @"YamlSamples\Prod\ships.yml";
            var pathToDirective = @"YamlSamples\Prod\directive.yml";

            var rawArgs = new[] {"-ships", pathToShips, "-directive", pathToDirective};

            var result = ArgsParser.ParseArgs(rawArgs);

            result.PathToShips.Should().Be(pathToShips);
            result.PathToDirective.Should().Be(pathToDirective);
        }

        [Fact]
        public void ParseArgs_WithSpacesInArgsName_ReturnsCorrectOutput()
        {
            var pathToShips = @"YamlSamples\Prod\ships.yml";
            var pathToDirective = @"YamlSamples\Prod\directive.yml";

            var rawArgs = new[] { "     -ships      ", pathToShips, "     -directive     ", pathToDirective };

            var result = ArgsParser.ParseArgs(rawArgs);

            result.PathToShips.Should().Be(pathToShips);
            result.PathToDirective.Should().Be(pathToDirective);
        }

        [Fact]
        public void ParseArgs_WithSpacesInArgsNamesAndValues_ReturnsCorrectOutput()
        {
            var pathToShips = @"YamlSamples\Prod\ships.yml";
            var pathToDirective = @"YamlSamples\Prod\directive.yml";

            var rawArgs = new[]
            {
                "     -ships      ", "           " + pathToShips + "           ", "     -directive     ",
                "           " + pathToDirective + "           "
            };

            var result = ArgsParser.ParseArgs(rawArgs);

            result.PathToShips.Should().Be(pathToShips);
            result.PathToDirective.Should().Be(pathToDirective);
        }

        [Fact]
        public void ParseArgs_WithMissingParameter_ThrowsException()
        {
            var rawArgs = new[] { "-ships", "path", "path" };

            var ex = Record.Exception(() => ArgsParser.ParseArgs(rawArgs));

            Assert.IsType(typeof(ArgsParserException), ex);
        }

        [Fact]
        public void ParseArgs_WithMissingValue_ThrowsException()
        {
            var rawArgs = new[] { "-ships", "path", "-directive" };

            var ex = Record.Exception(() => ArgsParser.ParseArgs(rawArgs));

            Assert.IsType(typeof(ArgsParserException), ex);
        }
    }
}