using System;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace DefaultNamespace
{
    public class TimedDestructionSystem : Subscription
    {
        [Inject] private float _lifespan;
        [Inject] private ISelfDestruction _selfDestruction;
        
        public override void Initialize()
        {
            Observable.Timer(TimeSpan.FromSeconds(_lifespan))
                .Subscribe(_selfDestruction.Destroy).AddTo(_disposer);
        }
    }
}