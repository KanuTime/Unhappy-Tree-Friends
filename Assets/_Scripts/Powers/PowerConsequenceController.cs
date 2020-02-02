using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerConsequenceController : Subscription, IPowerConsequenceController
	{
        [Inject] private IMousePositionController _mousePositionController;
        [Inject] private ISelectedPowerModel _selectedPowerModel;
        [Inject] private IConsequenceData _consequenceData;
        [Inject] private ISoundManager _soundManager;
        [Inject] private IManaData _manaData;
        [Inject] private IManaModel _manaModel;

        private readonly Subject<(ConsequenceType, ITileModel)> _subject = new Subject<(ConsequenceType, ITileModel)>();
        public IObservable<(ConsequenceType, ITileModel)> ConsequenceTileTrigger => _subject;

        public override void Initialize()
        {
            _mousePositionController.Clicked
                .Subscribe(ApplyConsequence)
                .AddTo(_disposer);
        }

        private void ApplyConsequence(ITileModel tileModel)
        {
            if (tileModel == null)
            {
                _soundManager.PlaySound(SoundType.UiSelectTileFail);
                return;
            }

            var selectedPower = _selectedPowerModel.SelectedPower.Value;
            var clickedEnvironmentType = tileModel.Type.Value;
            var canAffordPower = _manaModel.Mana.Value >= _manaData.CostsForPower(selectedPower);

            if (selectedPower == PowerType.None) Debug.Log("No Power Selected");

            if (!_selectedPowerModel.IsAvailable(selectedPower) || !canAffordPower)
            {
                _soundManager.PlaySound(SoundType.UiSelectTileFail);
                return;
            }

            _soundManager.PlaySound(SoundType.UiSelectTile);
            _manaModel.Mana.Value -= _manaData.CostsForPower(selectedPower);

            foreach (var consequenceSetup in _consequenceData.Consequences)
            {
                if (consequenceSetup._power == selectedPower && consequenceSetup._environment == clickedEnvironmentType)
                {
                    Debug.Log($"Applying consequence: {consequenceSetup._consequence}");
                    _subject.OnNext((consequenceSetup._consequence, tileModel));
                }
            }

            _selectedPowerModel.TriggerCooldown(selectedPower);
        }
    }
}