using Zenject;

namespace _Scripts.Utility
{
    public abstract class View : MonoInstaller
    {
        protected abstract void Install();
        
        public override void InstallBindings()
        {
            Install();
        }
        
        public virtual void ResetView() { }
    }
}