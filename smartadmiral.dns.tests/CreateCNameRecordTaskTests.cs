using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreateCNameRecordTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "name=hello hostalias=server.example.com zone=myzone";
            var expectedCommand = "Add-DnsServerResourceRecordCName -Name hello -HostNameAlias server.example.com -ZoneName myzone";
            var sut = new CreateCNameRecordTask(input, null);

            TestHelper.VerifyCommandTransformation(sut,input,expectedCommand);
        }
    }
}