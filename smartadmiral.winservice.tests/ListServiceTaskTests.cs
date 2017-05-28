using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.tests.common;
using smartadmiral.winservice.Tasks;
using Xunit;

namespace smartadmiral.winservice.tests
{
    public class ListServiceTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new ListServiceTask();
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript("Get-Service"), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
