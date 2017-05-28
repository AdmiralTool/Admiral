using System.Linq;
using System.Management.Automation;
using Moq;
using smartadmiral.common.Tasks;

namespace smartadmiral.tests.common
{
    public static class Extensions
    {
        public static void SetupScriptBuilderToReturnResult(this Mock<IPowerShellConnection> powerShellConnection,
            Mock<IPowerShellScriptBuilder> powerShellScriptBuilderMock,
            Mock<IPowerShellScriptExecutor> commandInvokerMock)
        {
            powerShellScriptBuilderMock.Setup(x => x.AddScript(It.IsAny<string>())).Returns(commandInvokerMock.Object);
            powerShellConnection.Setup(x => x.CreateScriptBuilder()).Returns(powerShellScriptBuilderMock.Object);
            var expectedResult = new PowerShellResult(true, Enumerable.Empty<PSObject>());
            commandInvokerMock.Setup(x => x.Invoke()).Returns(expectedResult);
        }
    }
}