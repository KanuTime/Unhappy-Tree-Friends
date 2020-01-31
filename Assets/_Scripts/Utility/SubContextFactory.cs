using System;
using UniRx;
using Zenject;

namespace _Scripts.Utility
{
    public interface ISubContextFactory
    {
        IDisposable Create<T>(params object[] args) where T : InstallerBase;
    }
    
    public class SubContextFactory : ISubContextFactory
    {
        private readonly DiContainer _container;

        public SubContextFactory(DiContainer container)
        {
            _container = container;
        }

        public IDisposable Create<T>(params object[] args) where T : InstallerBase
        {
            var disposer = new CompositeDisposable();

            var subContext = _container.CreateSubContainer();
            var installer = subContext.Instantiate<T>(args);
            
            subContext.Bind<ISelfDestruction>().FromInstance(new SelfDestruction(disposer.Dispose));
            
            installer.InstallBindings();
            
            SetupInitializableManager(subContext);
            SetupDisposableManager(subContext, disposer);
            SetupTickableManager(subContext, disposer);
            
            return disposer;
        }
        
        private void SetupTickableManager(DiContainer subContext, CompositeDisposable disposer)
        {
            var manager = subContext.Instantiate<TickableManager>();
            Observable.EveryUpdate().Subscribe(manager.Update).AddTo(disposer);
            Observable.EveryLateUpdate().Subscribe(manager.LateUpdate).AddTo(disposer);
            Observable.EveryFixedUpdate().Subscribe(manager.FixedUpdate).AddTo(disposer);
        }

        private void SetupDisposableManager(DiContainer subContext, CompositeDisposable disposer)
        {
            var manager = subContext.Instantiate<DisposableManager>();
            disposer.Add(Disposable.Create(() =>
            {
                manager.Dispose();
                manager.LateDispose();
            }));
        }

        private void SetupInitializableManager(DiContainer subContext)
        {
            var manager = subContext.Instantiate<InitializableManager>();
            manager.Initialize();
        }
    }
}