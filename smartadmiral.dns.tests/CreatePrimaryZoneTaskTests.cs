using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreatePrimaryZoneTaskTests
    {
        [Theory]
        [InlineData("name=myzone zonefile=myzonefile dynamicUpdate=yes",
             "Add-DnsServerPrimaryZone -Name myzone -ZoneFile myzonefile -DynamicUpdate NonsecureAndSecure -PassThru")]
        [InlineData("name=myzone1 zonefile=myzonefile1 dynamicUpdate=true",
             "Add-DnsServerPrimaryZone -Name myzone1 -ZoneFile myzonefile1 -DynamicUpdate NonsecureAndSecure -PassThru")]
        [InlineData("name=myzone zonefile=myzonefile dynamicUpdate=false",
             "Add-DnsServerPrimaryZone -Name myzone -ZoneFile myzonefile -PassThru")]
        [InlineData("name=myzone zonefile=myzonefile",
             "Add-DnsServerPrimaryZone -Name myzone -ZoneFile myzonefile -PassThru")]
        public void Execute_InvokesCorrectPowerShellCommand(string input, string expectedCommand)
        {
            var sut = new CreatePrimaryZoneTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}