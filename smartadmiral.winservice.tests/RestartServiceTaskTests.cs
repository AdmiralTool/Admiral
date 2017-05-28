using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.tests.common;
using smartadmiral.winservice.Tasks;
using Xunit;

namespace smartadmiral.winservice.tests
{
    public class RestartServiceTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new RestartServiceTask("servicename=spooler", null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript("Restart-Service spooler"), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
