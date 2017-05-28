using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.runcommand.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.runcommand.tests
{
    public class RunCommandTaskTests
    {
        [Fact]
        public void Execute_InvokesPowerShellCommand()
        {
            var expectedCommand = "echo test ; echo hello";
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new RunCommandTask(expectedCommand);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x=>x.AddScript(expectedCommand), Times.Once);
            scriptExecutorMock.Verify(x=>x.Invoke(), Times.Once);
        }
    }
}
