using FluentAssertions;
using smartadmiral.files.Creators;
using smartadmiral.files.Tasks;
using Xunit;

namespace smartadmiral.files.tests
{
    public class FilesTaskCreatorTests
    {
        [Fact]
        public void Create_WithCopyFileLocalString_ReturnsCopyFileLocalTask()
        {
            var taskName = "copyfilelocal";

            var sut = new FilesTaskCreator();
            var task = sut.Create(taskName, string.Empty, null);

            task.Should().BeOfType<CopyFileFromLocalTask>();
        }
    }
}