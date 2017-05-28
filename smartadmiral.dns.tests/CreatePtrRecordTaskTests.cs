using smartadmiral.dns.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dns.tests
{
    public class CreatePtrRecordTaskTests
    {
        [Fact]
        public void Execute_InvokesCorrectPowerShellCommand()
        {
            var input = "name=25 zone=\"1.168.192.in-arpa.zone\" ttl=\"01:00:00\" ptrname=\"myhost.example.com\"";
            var expectedCommand =
                "Add-DnsServerResourceRecordPtr -Name 25 -ZoneName \"1.168.192.in-arpa.zone\" -TimeToLive \"01:00:00\" -PtrDomainName \"myhost.example.com\"";
            var sut = new CreatePtrRecordTask(input, null);

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommand);
        }
    }
}