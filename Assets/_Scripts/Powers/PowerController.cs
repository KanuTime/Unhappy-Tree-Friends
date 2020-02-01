using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerController : Subscription
    {
        [Inject] private ISelectedPowerModel _selectedPowerModel;

        [Inject] private Button _button;
        [Inject] private PowerType _type;

        public override void Initialize()
        {
            _button.OnClickAsObservable().Subscribe(OnButtonClick).AddTo(_disposer);
        }

        private void OnButtonClick()
        {
            Debug.Log($"Button with type {_type} clicked.");
            _selectedPowerModel.SelectedPower.Value = _type;
        }
    }
}