using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Growth
{
    public class SpreadIncreaseSystem : Subscription
    {
        [Inject] private ISpread _spread;
        [Inject] private ITileModel _model;
        
        public override void Initialize()
        {
            _spread.Triggered.Subscribe(Spawn).AddTo(_disposer);
        }

        private void Spawn(Faction faction)
        {
            if (_model.Intensity(faction).Value == 0 && _model.Intensity(faction.Invert()).Value < 5)
                _model.Intensity(faction).Value = 1;
        }
    }
}