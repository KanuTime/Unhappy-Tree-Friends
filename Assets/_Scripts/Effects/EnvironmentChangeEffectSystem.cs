using System;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Effects
{
    public class EnvironmentChangeEffectSystem : Subscription
    {
        [Inject] private float _delay;
        [Inject] private IEffectSystem _effectSystem;
        
        public override void Initialize()
        {
            _effectSystem.EnvironmentChangeEffects.Subscribe(ChangeEnvironment).AddTo(_disposer);
        }

        private void ChangeEnvironment((ITileModel tile, EnvironmentType environment) tuple)
        {
            Observable.Timer(TimeSpan.FromSeconds(_delay))
                .Subscribe(_ => tuple.tile.Type.Value = tuple.environment).AddTo(_disposer);
        }
    }
}