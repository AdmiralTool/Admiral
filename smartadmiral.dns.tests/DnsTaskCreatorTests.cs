using System;
using FluentAssertions;
using smartadmiral.dns.Creators;
using smartadmiral.dns.Tasks;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class DnsTaskCreatorTests
    {
        [Theory]
        [InlineData("createarecord", typeof(CreateARecordTask))]
        [InlineData("createcnamerecord", typeof(CreateCNameRecordTask))]
        [InlineData("createforwarder", typeof(CreateForwarderTask))]
        [InlineData("createprimaryzone", typeof(CreatePrimaryZoneTask))]
        [InlineData("createptrrecord", typeof(CreatePtrRecordTask))]
        [InlineData("createreverselookup", typeof(CreateReverseLookupZoneTask))]
        [InlineData("createsecondaryzone", typeof(CreateSecondaryZoneTask))]
        [InlineData("removezone", typeof(RemoveZoneTask))]
        public void Create_WithCopyFileLocalString_ReturnsCopyFileLocalTask(string taskName, Type type)
        {
            var sut = new DnsTaskCreator();
            var task = sut.Create(taskName, "name=test", null);

            task.GetType().Should().Be(type);
        }
    }
}