using System.Collections.Generic;
using System.Linq;
using Moq;
using smartadmiral.common.Tasks;

namespace smartadmiral.tests.common
{
    public class TestHelper
    {
        public static void VerifyCommandTransformation(ITask sut, string input, string expectedOutput)
        {
            VerifyCommandTransformation(sut, input, new[] {expectedOutput});
        }

        public static void VerifyCommandTransformation(ITask sut, string input, IEnumerable<string> expectedOutputs)
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            sut.Execute(powerShellConnectionMock.Object);

            var outputsList = expectedOutputs.ToList();
            foreach (var expectedOutput in outputsList)
            {
                scriptBuilderMock.Verify(x => x.AddScript(expectedOutput), Times.Once);
            }
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Exactly(outputsList.Count()));
        }
    }
}