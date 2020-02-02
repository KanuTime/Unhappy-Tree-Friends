using _Scripts.Utility;
using _Scripts.Effects;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerPresenter : Subscription, ITickable
    {
        [Inject] private Button _button;
        [Inject] private Image _buttonImage;
        [Inject] private Image _cooldownImage;
        [Inject] private PowerType _powerType;
        [Inject] private ISelectedPowerModel _selectedPowerModel;
        [Inject] private IPowerData _powerData;
        [Inject] private ISoundManager _soundManager;

        private Color _originalColor;

        public override void Initialize()
        {
            _originalColor = _buttonImage.color;

            _selectedPowerModel.SelectedPower
                .Subscribe(SelectedChanged)
                .AddTo(_disposer);
        }

        private void SelectedChanged(PowerType powerType)
        {
            _buttonImage.color = _powerType == powerType ? Color.white : _originalColor;
        }

        public void Tick()
        {
            _cooldownImage.fillAmount = _selectedPowerModel.Cooldown(_powerType) / _powerData.Cooldown(_powerType);
            
            if (_cooldownImage.fillAmount == 0)
            {
                _soundManager.PlaySound(SoundType.PowerReady);
            }
        }
    }
}
