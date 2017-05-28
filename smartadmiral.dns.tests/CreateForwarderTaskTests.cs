using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreateForwarderTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "ip=192.168.1.1";
            var expectedCommand = "Add-DnsServerForwarder -IPAddress 192.168.1.1 -PassThru";
            var sut = new CreateForwarderTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}