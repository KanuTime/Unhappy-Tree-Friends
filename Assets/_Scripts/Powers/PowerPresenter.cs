using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerPresenter : Subscription
    {
        [Inject] private ISelectedPowerModel _selectedPowerModel;


        public override void Initialize()
        {
            _selectedPowerModel.SelectedPower.Subscribe(OnSelectedPowerChanged).AddTo(_disposer);
        }

        private void OnSelectedPowerChanged(PowerType selectedPowerType)
        {
            Debug.Log($"Selected power: {selectedPowerType}");
        }
    }
}