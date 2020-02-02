using System;
using System.Linq;
using _Scripts.Factions;
using _Scripts.Powers;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public interface IEffectSystem
    {
        UniRx.IObservable<(ITileModel, EnvironmentType)> EnvironmentChangeEffects { get; }
        UniRx.IObservable<(ITileModel, Faction, int)> KillEffects { get; }
        UniRx.IObservable<(ITileModel, Faction, int)> SpawnEffects { get; }
    }
    
    public class EffectSystem : Subscription, IEffectSystem
    {
        [Inject] private IEffectData effectData;
        [Inject] private IPowerConsequenceController powerConsequenceController;

        public override void Initialize()
        {
            powerConsequenceController.ConsequenceTileTrigger
                .Subscribe(ApplyEffect)
                .AddTo(_disposer);
        }

        private void ApplyEffect((ConsequenceType consequenceType, ITileModel tileModel) consequenceTuple)
        {
            var (consequence, tile) = consequenceTuple;
            var tileEnvironment = tile.Type.Value;
            
            tile.Consequence.OnNext(consequence);

            var appliedEffects = effectData.Effects
                .Where(data => data.ConsequenceType == consequence
                                && data.Environment == tileEnvironment)
                .Select(data => data.Effects)
                .FirstOrDefault();

            if (appliedEffects == null) return;

            foreach (var effectEntry in appliedEffects)
            {
                TriggerEffect(effectEntry, tile);
                
                Debug.Log($"Applying effect: {effectEntry.Effect}");
                if (effectEntry.Effect == EffectType.EnvironmentChange)
                {
                    Debug.Log($"Changing environment to {effectEntry.Environment}");
                }
            }
        }

        private void TriggerEffect(EffectData effectEntry, ITileModel tile)
        {
            switch (effectEntry.Effect)
            {
                case EffectType.EnvironmentChange:
                    _environmentChangeEffects.OnNext((tile, effectEntry.Environment));
                    break;
                case EffectType.KillTree:
                    _killEffects.OnNext((tile, Faction.Nature, effectEntry.Intensity));
                    break;
                case EffectType.KillHuman:
                    _killEffects.OnNext((tile, Faction.Humans, effectEntry.Intensity));
                    break;
                case EffectType.PlantTree:
                    _spawnEffects.OnNext((tile, Faction.Nature, effectEntry.Intensity));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private readonly ISubject<(ITileModel, EnvironmentType)> _environmentChangeEffects = new Subject<(ITileModel, EnvironmentType)>();
        public UniRx.IObservable<(ITileModel, EnvironmentType)> EnvironmentChangeEffects => _environmentChangeEffects;
        
        private readonly ISubject<(ITileModel, Faction, int)> _killEffects = new Subject<(ITileModel, Faction, int)>();
        public UniRx.IObservable<(ITileModel, Faction, int)> KillEffects => _killEffects;
        
        private readonly ISubject<(ITileModel, Faction, int)> _spawnEffects = new Subject<(ITileModel, Faction, int)>();
        public UniRx.IObservable<(ITileModel, Faction, int)> SpawnEffects => _spawnEffects;
    }
}