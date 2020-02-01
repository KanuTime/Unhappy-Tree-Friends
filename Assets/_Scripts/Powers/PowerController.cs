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
        [Inject] private Text _text;
        [Inject] private PowerType _type;

        public override void Initialize()
        {
            _text.text = _type.ToString();
            _button.OnClickAsObservable().Subscribe(OnButtonClick).AddTo(_disposer);
        }

        private void OnButtonClick()
        {
            Debug.Log($"Button with type {_type} clicked.");
        }
    }
}