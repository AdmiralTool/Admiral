using FluentAssertions;
using smartadmiral.runcommand.Creators;
using smartadmiral.runcommand.Tasks;
using Xunit;

namespace smartadmiral.runcommand.tests
{
    public class RunCommandTaskCreatorTests
    {
        [Fact]
        public void Create_WithRunCommandString_ReturnsRunCommandTask()
        {
            var taskName = "runcommand";

            var sut = new RunCommandTaskCreator();
            var task = sut.Create(taskName, string.Empty, null);

            task.Should().BeOfType<RunCommandTask>();
        }

        [Fact]
        public void Create_WithRunScriptString_ReturnsRunCommandTask()
        {
            var taskName = "powershell-script";

            var sut = new RunCommandTaskCreator();
            var task = sut.Create(taskName, "path=blah", null);

            task.Should().BeOfType<RunScriptTask>();
        }
    }
}