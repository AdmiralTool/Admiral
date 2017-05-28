using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.dns.Bootstrap
{
    public class DnsExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, DnsModule>(DnsModule.MODULE_NAME);
        }
    }
}