using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerConsequenceController : Subscription
    {
        [Inject] private IMousePositionController _mousePositionController;
        [Inject] private ISelectedPowerModel _selectedPowerModel;

        public override void Initialize()
        {
            _mousePositionController.Clicked
                .Subscribe(ApplyConsequence)
                .AddTo(_disposer);
        }

        private void ApplyConsequence(ITileModel tileModel)
        {
            // TODO: choose consequence from config
            var consequence = ConsequenceType.Earthquake;
            Debug.Log($"Applying consequence: {consequence}");
        }
    }
}