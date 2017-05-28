using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.windowsfeature.Bootstrap
{
    public class WindowsFeatureExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, WindowsFeatureModule>(WindowsFeatureModule.MODULE_NAME);
        }
    }
}
