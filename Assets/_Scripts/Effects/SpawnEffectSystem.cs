using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public class SpawnEffectSystem : Subscription
    {
        [Inject] private IEffectSystem _effectSystem;
        
        public override void Initialize()
        {
            _effectSystem.SpawnEffects.Subscribe(Spawn).AddTo(_disposer);
        }

        private void Spawn((ITileModel tile, Faction faction, int amount) tuple)
        {
            var (tile, faction, amount) = tuple;
            if (faction == Faction.Nature)
            {
                var desiredAmount = Mathf.Min(5, tile.Intensity(faction).Value + amount);
                var enemies = tile.Intensity(faction.Invert()).Value;
                if (enemies + desiredAmount > 5)
                {
                    desiredAmount = 5 - enemies;
                }
                tile.Intensity(faction).Value = desiredAmount;    
            }
        }
    }
}