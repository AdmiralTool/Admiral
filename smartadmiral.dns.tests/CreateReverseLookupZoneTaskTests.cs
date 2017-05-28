using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreateReverseLookupZoneTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "networkid=\"192.168.1.0/24\" zonefile=\"1.168.192.in-arpa.zone.dns\"";
            var expectedCommand = "Add-DnsServerPrimaryZone -NetworkID \"192.168.1.0/24\" -ZoneFile \"1.168.192.in-arpa.zone.dns\"";
            var sut = new CreateReverseLookupZoneTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}