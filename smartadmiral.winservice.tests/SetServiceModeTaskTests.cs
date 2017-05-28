using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.tests.common;
using smartadmiral.winservice.Tasks;
using Xunit;

namespace smartadmiral.winservice.tests
{
    public class SetServiceModeTaskTests
    {
        [Theory]
        [InlineData("servicename=spooler mode=auto", "Set-Service spooler -StartupType Automatic")]
        [InlineData("servicename=spooler mode=manual", "Set-Service spooler -StartupType Manual")]
        [InlineData("servicename=spooler mode=disabled", "Set-Service spooler -StartupType Disabled")]
        public void Execute_InvokesCorrectPowerShellCommand(string args, string expectedCommand)
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new SetServiceModeTask(args, null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript(expectedCommand), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
