using System.IO;
using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.runcommand.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.runcommand.tests
{
    public class RunScriptTaskTests
    {
        [Fact]
        public void Execute_InvokesPowerShelScriptFromFile()
        {
            var expectedCommand = "echo test ; echo hello";
            var fileName = "test.ps1";
            File.WriteAllText(fileName, expectedCommand);

            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new RunScriptTask($"path={fileName}", null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript(expectedCommand), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}