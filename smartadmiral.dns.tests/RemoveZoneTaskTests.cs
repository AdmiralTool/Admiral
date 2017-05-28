using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class RemoveZoneTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "name=myzone";
            var expectedCommand = "Remove-DnsServerZone myzone -PassThru";
            var sut = new RemoveZoneTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}