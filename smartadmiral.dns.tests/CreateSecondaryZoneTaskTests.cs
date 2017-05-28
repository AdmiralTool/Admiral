using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreateSecondaryZoneTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "name=myzone master=\"192.168.1.1,192.18.1.2,etc\" zonefile=X";
            var expectedCommand = "Add-DnsServerSecondaryZone -Name myzone -ZoneFile X -MasterServers \"192.168.1.1,192.18.1.2,etc\" -PassThru";
            var sut = new CreateSecondaryZoneTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}