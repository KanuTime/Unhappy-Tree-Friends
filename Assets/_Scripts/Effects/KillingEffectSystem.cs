using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public class KillingEffectSystem : Subscription
    {
        [Inject] private IEffectSystem _effectSystem;
        
        public override void Initialize()
        {
            _effectSystem.KillEffects.Subscribe(Kill).AddTo(_disposer);
        }

        private void Kill((ITileModel tile, Faction faction, int amount) tuple)
        {
            var (tile, faction, amount) = tuple;
            tile.Intensity(faction).Value = Mathf.Max(0, tile.Intensity(faction).Value - amount);
        }
    }
}