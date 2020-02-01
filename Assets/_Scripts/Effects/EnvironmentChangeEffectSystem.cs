using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Effects
{
    public class EnvironmentChangeEffectSystem : Subscription
    {
        [Inject] private IEffectSystem _effectSystem;
        
        public override void Initialize()
        {
            _effectSystem.EnvironmentChangeEffects.Subscribe(ChangeEnvironment).AddTo(_disposer);
        }

        private void ChangeEnvironment((ITileModel tile, EnvironmentType environment) tuple)
        {
            tuple.tile.Type.Value = tuple.environment;
        }
    }
}