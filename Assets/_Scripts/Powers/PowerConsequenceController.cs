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
                return;

            var selectedPower = _selectedPowerModel.SelectedPower.Value;
            var clickedEnvironmentType = tileModel.Type.Value;

            if (selectedPower == PowerType.None) Debug.Log("No Power Selected");

            if (!_selectedPowerModel.IsAvailable(selectedPower)) return;

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