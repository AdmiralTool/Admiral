using System.Collections.Generic;
using smartadmiral.common;
using smartadmiral.dhcp.Tasks;
using smartadmiral.tests.common;
using Xunit;

namespace smartadmiral.dhcp.tests
{
    public class CreateNewPoolTaskTests
    {
        public static IEnumerable<object[]> ExpectedCommands()
        {
            yield return new object[]
            {
                "name=myscope startRange=192.168.1.1 endRange=192.168.1.150 subnetMask=255.255.255.0",
                new [] { "Add-DhcpServerV4Scope -Name myscope -StartRange 192.168.1.1 -EndRange 192.168.1.150 -SubnetMask 255.255.255.0" }
            };
            yield return new object[]
            {
                "name=myscope startRange=192.168.1.1 endRange=192.168.1.150 subnetMask=255.255.255.0 dnsserver=192.168.1.1",
                new []
                {
                    "Add-DhcpServerV4Scope -Name myscope -StartRange 192.168.1.1 -EndRange 192.168.1.150 -SubnetMask 255.255.255.0",
                    "Set-DhcpServerV4OptionValue -DnsServer 192.168.1.1"
                }
            };
            yield return new object[]
            {
                "name=myscope startRange=192.168.1.1 endRange=192.168.1.150 subnetMask=255.255.255.0 dnsserver=192.168.1.1 dnsdomain=\"example.com\"",
                new []
                {
                    "Add-DhcpServerV4Scope -Name myscope -StartRange 192.168.1.1 -EndRange 192.168.1.150 -SubnetMask 255.255.255.0",
                    "Set-DhcpServerV4OptionValue -DnsServer 192.168.1.1 -DnsDomain \"example.com\""
                }
            };
            yield return new object[]
            {
                "name=myscope startRange=192.168.1.1 endRange=192.168.1.150 subnetMask=255.255.255.0 dnsserver=192.168.1.1 dnsdomain=\"example.com\" gateway=192.168.1.254",
                new []
                {
                    "Add-DhcpServerV4Scope -Name myscope -StartRange 192.168.1.1 -EndRange 192.168.1.150 -SubnetMask 255.255.255.0",
                    "Set-DhcpServerV4OptionValue -DnsServer 192.168.1.1 -DnsDomain \"example.com\" -Router 192.168.1.254"
                }
            };

            yield return new object[]
            {
                "name=myscope startRange=192.168.1.1 endRange=192.168.1.150 subnetMask=255.255.255.0 dnsserver=192.168.1.1 dnsdomain=\"example.com\" gateway=192.168.1.254 leasedays=3",
                new []
                {
                    "Add-DhcpServerV4Scope -Name myscope -StartRange 192.168.1.1 -EndRange 192.168.1.150 -SubnetMask 255.255.255.0",
                    "Set-DhcpServerV4OptionValue -DnsServer 192.168.1.1 -DnsDomain \"example.com\" -Router 192.168.1.254",
                    "Set-DhcpServerv4Scope -ScopeId hostname -LeaseDuration (New-TimeSpan -Days 3)"
                }
            };
        }

        [Theory]
        [MemberData(nameof(ExpectedCommands))]
        public void Execute_InvokesCorrectPowerShellCommand(string input, IEnumerable<string> expectedCommands)
        {
            var sut = new CreateNewPoolTask(input, new AdmiralContext {Hostname = "hostname"});

            TestHelper.VerifyCommandTransformation(sut, input, expectedCommands);
        }
    }
}