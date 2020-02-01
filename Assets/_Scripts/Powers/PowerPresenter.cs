using _Scripts.Utility;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerPresenter : Subscription
    {
        [Inject] private ISelectedPowerModel _selectedPowerModel;
        [Inject] private PowerType _powerType;
        [Inject] private Button _button;
        [Inject] private Text _text;

        public override void Initialize()
        {
            _text.text = _powerType.ToString();
            _selectedPowerModel.SelectedPower.Subscribe(OnSelectedPowerChanged).AddTo(_disposer);
        }

        private void OnSelectedPowerChanged(PowerType selectedPowerType)
        {
            if (selectedPowerType == _powerType)
            {
            }
        }
    }
}