using FluentAssertions;
using smartadmiral.windowsfeature.Creators;
using smartadmiral.windowsfeature.Tasks;
using Xunit;

namespace smartadmiral.windowsfeature.tests
{
    public class WindowsFeatureTaskCreatorTests
    {
        [Fact]
        public void Create_WithInstallFeatureString_ReturnsInstallWindowsFeatureTask()
        {
            var taskName = "installfeature";

            var sut = new WindowsFeatureTaskCreator();
            var task = sut.Create(taskName, "name=test", null);

            task.Should().BeOfType<InstallWindowsFeatureTask>();
        }

        [Fact]
        public void Create_WithUninstallFeatureString_ReturnsUninstallWindowsFeatureTask()
        {
            var taskName = "uninstallfeature";

            var sut = new WindowsFeatureTaskCreator();
            var task = sut.Create(taskName, "name=test", null);

            task.Should().BeOfType<UninstallWindowsFeatureTask>();
        }
    }
}