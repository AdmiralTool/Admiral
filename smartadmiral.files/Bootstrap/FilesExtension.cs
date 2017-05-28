using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.files.Bootstrap
{
    public class FilesExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, FilesModule>(FilesModule.MODULE_NAME);
        }
    }
}