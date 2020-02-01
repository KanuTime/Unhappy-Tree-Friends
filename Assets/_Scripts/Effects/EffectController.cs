using System.Linq;
using _Scripts.Powers;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public class EffectController : Subscription
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

            var appliedEffects = effectData.Effects
                .Where(data => data.ConsequenceType == consequence
                                && data.Environment == tileEnvironment)
                .Select(data => data.Effects)
                .FirstOrDefault();

            if (appliedEffects == null) return;

            foreach (var effectEntry in appliedEffects)
            {
                Debug.Log($"Applying effect: {effectEntry.Effect}");
                if (effectEntry.Effect == EffectType.EnvironmentChange)
                {
                    Debug.Log($"Changing environment to {effectEntry.Environment}");
                }
            }
        }
    }
}