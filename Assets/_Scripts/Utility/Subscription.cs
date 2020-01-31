using System;
using UniRx;
using Zenject;

namespace _Scripts.Utility
{
    public abstract class Subscription : IDisposable, IInitializable
    {
        protected readonly CompositeDisposable _disposer = new CompositeDisposable();

        public void Dispose()
        {
            _disposer.Dispose();
        }

        public abstract void Initialize();
    }
}