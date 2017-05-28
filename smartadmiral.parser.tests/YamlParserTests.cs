using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using smartadmiral.common.Directives;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using Xunit;

namespace smartadmiral.parser.tests
{
    public class YamlParserTests
    {
        private readonly IFixture _fixture;

        public YamlParserTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private const string DIRECTIVE_COMMON_FILE_NAME = @"samples\directive_common.yml";
        private const string DIRECTIVE_CHILD_FILE_NAME = @"samples\directive_child.yml";
        private const string DIRECTIVE_GRANDCHILD_FILE_NAME = @"samples\directive_grandchild.yml";

        private const string SHIPS_FILE_NAME = @"samples\ships.yml";

        public static IEnumerable<object[]> ExpectedHostnames()
        {
            yield return new object[]
            {
                DIRECTIVE_COMMON_FILE_NAME,
                new[] {"192.168.40.228", "192.168.40.226"}
            };
            yield return new object[]
            {
                DIRECTIVE_CHILD_FILE_NAME,
                new[] {"192.168.40.228", "192.168.40.226", "web.iis.shmiis", "secondhost"}
            };
            yield return new object[]
            {
                DIRECTIVE_GRANDCHILD_FILE_NAME,
                new[]
                {
                    "192.168.40.228", "192.168.40.226", "web.iis.shmiis", "secondhost", "web.iis.shmiis11",
                    "secondhost11"
                }
            };
        }

        public static IEnumerable<object[]> ExpectedCredentials()
        {
            yield return new object[]
            {
                DIRECTIVE_COMMON_FILE_NAME,
                new Credentials("common_admin", "password_common")
            };
            yield return new object[]
            {
                DIRECTIVE_CHILD_FILE_NAME,
                new Credentials("child_admin", "password_child"),
            };
            yield return new object[]
            {
                DIRECTIVE_GRANDCHILD_FILE_NAME,
                new Credentials("very_child_admin", "password_very_child"),
            };
        }

        [Theory]
        [MemberData(nameof(ExpectedHostnames))]
        public void Parse_ReturnsDirective_WithCorrectHostnames(string directiveFileName, IEnumerable<string> expectedHostnames)
        {
            var sut = _fixture.Create<YamlParser>();

            var result = sut.Parse(SHIPS_FILE_NAME, directiveFileName);

            result.Hostnames.ShouldBeEquivalentTo(expectedHostnames);
        }

        [Theory]
        [MemberData(nameof(ExpectedCredentials))]
        public void Parse_ReturnsDirective_WithCorrectCredentials(string directiveFileName, Credentials expectedCredentials)
        {
            var sut = _fixture.Create<YamlParser>();

            var result = sut.Parse(SHIPS_FILE_NAME, directiveFileName);

            result.Credentials.ShouldBeEquivalentTo(expectedCredentials);
        }

        [Fact]
        public void Parse_ReturnsDirective_WithCorrectTaskDescriptions()
        {
            var listServiceTaskDescription = new TaskDescription("service", "listsrv", "name=spooler1 action=restart");
            var stopServiceTaskDescription = new TaskDescription("service", "stopsrv", "name=spooler2 action=stop");

            var moduleMock = _fixture.Freeze<Mock<IModule>>();
            moduleMock.SetupGet(module => module.Name).Returns("service");

            var sut = _fixture.Create<YamlParser>();

            var result = sut.Parse(SHIPS_FILE_NAME, DIRECTIVE_COMMON_FILE_NAME);

            result.TaskDescriptions.ShouldBeEquivalentTo(new[] { listServiceTaskDescription, stopServiceTaskDescription });
        }
    }
}