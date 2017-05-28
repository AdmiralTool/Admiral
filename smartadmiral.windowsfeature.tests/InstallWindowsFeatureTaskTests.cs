using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.tests.common;
using smartadmiral.windowsfeature.Tasks;
using Xunit;

namespace smartadmiral.windowsfeature.tests
{
    public class InstallWindowsFeatureTaskTests
    {
        [Theory]
        [InlineData("name=DHCP include-sub-features=true include-management-tools=true", "Install-WindowsFeature -Name DHCP -IncludeAllSubFeature -IncludeManagementTools")]
        [InlineData("name=DHCP include-sub-features=false include-management-tools=true", "Install-WindowsFeature -Name DHCP -IncludeManagementTools")]
        [InlineData("name=DHCP include-management-tools=true", "Install-WindowsFeature -Name DHCP -IncludeManagementTools")]
        [InlineData("name=DHCP include-sub-features=true include-management-tools=false", "Install-WindowsFeature -Name DHCP -IncludeAllSubFeature")]
        [InlineData("name=DHCP include-sub-features=true", "Install-WindowsFeature -Name DHCP -IncludeAllSubFeature")]
        [InlineData("name=DHCP", "Install-WindowsFeature -Name DHCP")]
        public void Execute_InvokesInstallPowerShellCommand(string input, string expectedCommand)
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new InstallWindowsFeatureTask(input, null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript(expectedCommand), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }

        [Theory]
        [InlineData("name=DHCP include-management-tools=true", "Uninstall-WindowsFeature -Name DHCP -IncludeManagementTools")]
        [InlineData("name=DHCP include-management-tools=false", "Uninstall-WindowsFeature -Name DHCP")]
        [InlineData("name=DHCP", "Uninstall-WindowsFeature -Name DHCP")]
        public void Execute_InvokesUninstallPowerShellCommand(string input, string expectedCommand)
        {
            var scriptBuilderMock = new Mock<IPowerShellScriptBuilder>();
            var scriptExecutorMock = new Mock<IPowerShellScriptExecutor>();
            var powerShellConnectionMock = new Mock<IPowerShellConnection>();
            powerShellConnectionMock.SetupScriptBuilderToReturnResult(scriptBuilderMock, scriptExecutorMock);

            var sut = new UninstallWindowsFeatureTask(input, null);
            sut.Execute(powerShellConnectionMock.Object);

            scriptBuilderMock.Verify(x => x.AddScript(expectedCommand), Times.Once);
            scriptExecutorMock.Verify(x => x.Invoke(), Times.Once);
        }
    }
}
