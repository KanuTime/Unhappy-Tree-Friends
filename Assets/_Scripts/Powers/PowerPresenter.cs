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
        [Inject] private Text _text;
        [Inject] private Sprite _sprite;

        public override void Initialize()
        {
            _text.text = _powerType.ToString();
            _image.sprite = _sprite;
        }
    }
}