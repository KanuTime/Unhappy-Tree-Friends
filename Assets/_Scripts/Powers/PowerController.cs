using _Scripts.Effects;
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
        [Inject] private Button _button;
        [Inject] private IMousePositionController _positionController;
        [Inject] private ISelectedPowerModel _selectedPowerModel;
        [Inject] private PowerType _type;
        [Inject] private ISoundManager soundManager;

        public override void Initialize()
        {
            _button.OnClickAsObservable().Subscribe(OnButtonClick).AddTo(_disposer);
            _positionController.Clicked.Subscribe(OnMouseClick).AddTo(_disposer);
        }

        private void OnButtonClick()
        {
            Debug.Log($"Button with type {_type} clicked.");
            _selectedPowerModel.SelectedPower.Value = _type;
            soundManager.UISelectTile();
        }

        private void OnMouseClick(ITileModel model)
        {
            if (model == null)
            {
                _selectedPowerModel.SelectedPower.Value = PowerType.None;
                return;
            }
        }
    }
}