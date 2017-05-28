using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.winservice.Bootstrap
{
    public class WinServiceExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, WindowsServiceModule>(WindowsServiceModule.MODULE_NAME);
        }
    }
}