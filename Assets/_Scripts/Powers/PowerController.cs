using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerController : Subscription
    {
        [Inject] private Button _button;
        [Inject] private ISelectedPowerModel _selectedPowerModel;
        [Inject] private PowerType _type;
        [Inject] private ISoundManager soundManager;

        public override void Initialize()
        {
            _button.OnClickAsObservable().Subscribe(OnButtonClick).AddTo(_disposer);
        }

        private void OnButtonClick()
        {
            _selectedPowerModel.SelectedPower.Value =
                _selectedPowerModel.SelectedPower.Value == _type ? PowerType.None : _type;

            soundManager.UISelectTile();
        }
    }
}