using Moq;
using smartadmiral.common.Tasks;
using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreateARecordTaskTests
    {
        [Theory]
        [InlineData("host=myhost zone=myzone ttl=01:00:00 ip=192.168.1.1 createptr=yes",
            "Add-DnsServerResourceRecordA -Name myhost -ZoneName myzone -IPv4Address 192.168.1.1 -TimeToLive 01:00:00 -CreatePtr")]
        [InlineData("host=myhost zone=zone ttl=01:00:00 ip=191.168.1.2 createptr=true",
            "Add-DnsServerResourceRecordA -Name myhost -ZoneName zone -IPv4Address 191.168.1.2 -TimeToLive 01:00:00 -CreatePtr")]
        [InlineData("host=myhost zone=myz0ne ttl=01:00:00 ip=192.169.1.1 createptr=false",
            "Add-DnsServerResourceRecordA -Name myhost -ZoneName myz0ne -IPv4Address 192.169.1.1 -TimeToLive 01:00:00")]
        [InlineData("host=myhost zone=myzone ttl=01:00:00 ip=192.168.1.1",
            "Add-DnsServerResourceRecordA -Name myhost -ZoneName myzone -IPv4Address 192.168.1.1 -TimeToLive 01:00:00")]
        public void Execute_InvokesCorrectPowerShellCommand(string input, string expectedCommand)
        {
            var sut = new CreateARecordTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}
