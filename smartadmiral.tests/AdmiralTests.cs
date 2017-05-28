using System.Linq;
using System.Security;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using smartadmiral.common;
using smartadmiral.common.Directives;
using smartadmiral.common.Parsers;
using smartadmiral.common.Tasks;
using smartadmiral.console.Core;
using Xunit;

namespace smartadmiral.tests
{
    public class AdmiralTests
    {
        public AdmiralTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private readonly IFixture _fixture;

        [Fact]
        public void Run_ConnectsToEachHost()
        {
            var directive = _fixture.Create<Directive>();
            var connectorMock = _fixture.Freeze<Mock<IConnector>>();
            var parserMock = _fixture.Freeze<Mock<IParser>>();
            parserMock.Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<string>())).Returns(directive);

            var sut = _fixture.Create<Admiral>();

            sut.Run(string.Empty, string.Empty);

            foreach (var directiveHostname in directive.Hostnames)
            {
                connectorMock.Verify(
                    connector =>
                        connector.Connect(directiveHostname, directive.Credentials.Username,
                            It.IsAny<SecureString>()), Times.Once);
            }
        }

        [Fact]
        public void Run_ExecutesTasksWithGivenDirective()
        {
            var tasksExecutor = _fixture.Freeze<Mock<TasksExecutor>>();
            var directive = _fixture.Create<Directive>();
            var parserMock = _fixture.Freeze<Mock<IParser>>();
            parserMock.Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<string>())).Returns(directive);

            var sut = new Admiral(parserMock.Object, _fixture.Create<IConnector>(), tasksExecutor.Object);
            sut.Run(string.Empty, string.Empty);

            tasksExecutor.Verify(
                x => x.Execute(directive, It.IsAny<IPowerShellConnection>(), It.IsAny<AdmiralContext>()),
                Times.Exactly(directive.Hostnames.Count()));
        }
    }
}