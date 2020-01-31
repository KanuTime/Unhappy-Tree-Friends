using Zenject;

namespace _Scripts.Utility
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISubContextFactory>().FromMethod(context => new SubContextFactory(context.Container)).AsTransient();
        }
    }
}