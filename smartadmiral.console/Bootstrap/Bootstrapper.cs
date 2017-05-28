using Microsoft.Practices.Unity;
using smartadmiral.console.Core;
using smartadmiral.dhcp.Bootstrap;
using smartadmiral.dns.Bootstrap;
using smartadmiral.files.Bootstrap;
using smartadmiral.parser.Bootstrap;
using smartadmiral.runcommand.Bootstrap;
using smartadmiral.windowsfeature.Bootstrap;
using smartadmiral.winservice.Bootstrap;

namespace smartadmiral.console.Bootstrap
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IConnector, Connector>();
            Container.AddExtension(new YamlParserExtension());
            Container.AddExtension(new RunCommandTasksExtension());
            Container.AddExtension(new WindowsFeatureExtension());
            Container.AddExtension(new WinServiceExtension());
            Container.AddExtension(new FilesExtension());
            Container.AddExtension(new DnsExtension());
            Container.AddExtension(new DhcpExtension());
        }
    }
}