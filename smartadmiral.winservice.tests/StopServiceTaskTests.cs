using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.tests.common;
using smartadmiral.winservice.Tasks;
using Xunit;

namespace smartadmiral.winservice.tests
{
    public class StopServiceTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new StopServiceTask("servicename=spooler", null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript("Stop-Service spooler"), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
