using System;
using FluentAssertions;
using smartadmiral.winservice.Creators;
using smartadmiral.winservice.Tasks;
using Xunit;

namespace smartadmiral.winservice.tests
{
    public class WinServiceTaskCreatorTests
    {
        [Theory]
        [InlineData("list-services", typeof(ListServiceTask))]
        [InlineData("start-service", typeof(StartServiceTask))]
        [InlineData("stop-service", typeof(StopServiceTask))]
        [InlineData("restart-service", typeof(RestartServiceTask))]
        [InlineData("set-service", typeof(SetServiceModeTask))]
        public void Create_WithCopyFileLocalString_ReturnsCopyFileLocalTask(string taskName, Type type)
        {
            var sut = new WinServiceTaskCreator();
            var task = sut.Create(taskName, "name=test", null);

            task.GetType().Should().Be(type);
        }
    }
}
