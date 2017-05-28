using FluentAssertions;
using smartadmiral.parser.DTOs.Directives;
using Xunit;

namespace smartadmiral.parser.tests
{
    public class DirectiveParserTests
    {
        private const string DIRECTIVE_COMMON_FILE_NAME = @"samples\directive_common.yml";
        private const string DIRECTIVE_WITH_SPACES_FILE_NAME = @"samples\directive_spaced.yml";

        [Fact]
        public void ParseDirective_ReturnsDirective_WithCorrectShipsGroup()
        {
            var sut = new DirectiveParser();

            var result = sut.ParseDirective(DIRECTIVE_COMMON_FILE_NAME);

            result.Ships.Should().Be("common");
        }

        [Fact]
        public void ParseDirective_ReturnsDirective_WithCorrecUsername()
        {
            var sut = new DirectiveParser();

            var result = sut.ParseDirective(DIRECTIVE_COMMON_FILE_NAME);

            result.Username.Should().Be("common_admin");
        }

        [Fact]
        public void ParseDirective_ReturnsDirective_WithCorrectPassword()
        {
            var sut = new DirectiveParser();

            var result = sut.ParseDirective(DIRECTIVE_COMMON_FILE_NAME);

            result.Password.Should().Be("password_common");
        }

        [Fact]
        public void ParseDirective_ReturnsDirective_WithCorrectTasks()
        {
            var expectedTasks = new[]
            {
                new TaskDto { Name = "list services", Module = "service.listsrv", Args = "name=spooler1 action=restart"},
                new TaskDto { Name = "stop service", Module = "service.stopsrv", Args = "name=spooler2 action=stop"},
            };
            var sut = new DirectiveParser();

            var result = sut.ParseDirective(DIRECTIVE_COMMON_FILE_NAME);

            result.Tasks.ShouldBeEquivalentTo(expectedTasks);
        }

        [Fact]
        public void ParseDirective_ReturnsDirective_WithCorrectTasks_IfArgsContainSpaces()
        {
            var expectedTasks = new[]
            {
                new TaskDto { Name = "list services", Module = "service.listsrv", Args = "name=spooler1 action='we have spaces, so whut'"},
                new TaskDto { Name = "stop service", Module = "service.stopsrv", Args = "name='wot m8' action=stop"},
            };
            var sut = new DirectiveParser();

            var result = sut.ParseDirective(DIRECTIVE_WITH_SPACES_FILE_NAME);

            result.Tasks.ShouldBeEquivalentTo(expectedTasks);
        }
    }
}