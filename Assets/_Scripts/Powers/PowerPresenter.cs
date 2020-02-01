using _Scripts.Utility;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerPresenter : Subscription
    {
        [Inject] private Button _button;
        [Inject] private Image _image;
        [Inject] private PowerType _powerType;
        [Inject] private ISelectedPowerModel _selectedPowerModel;

        public override void Initialize()
        {
            _selectedPowerModel.SelectedPower
                .Subscribe(SelectedChanged)
                .AddTo(_disposer);
        }

        private void SelectedChanged(PowerType powerType)
        {
            _image.color = _powerType == powerType ? Color.green : Color.white;
        }
    }
}