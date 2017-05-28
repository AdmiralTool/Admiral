using Microsoft.Practices.Unity;
using smartadmiral.common.Modules;

namespace smartadmiral.runcommand.Bootstrap
{
    public class RunCommandTasksExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IModule, RunCommandModule>(RunCommandModule.MODULE_NAME);
        }
    }
}
