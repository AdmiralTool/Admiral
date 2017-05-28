using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.dhcp.Bootstrap
{
    public class DhcpExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, DhcpModule>(DhcpModule.MODULE_NAME);
        }
    }
}