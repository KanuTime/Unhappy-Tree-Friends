using System;
using _Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerView : View
    {
        [Inject] private Canvas _canvas;
        [Inject] private PowerType _powerType;
        
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        [SerializeField] private Sprite _windPower;
        [SerializeField] private Sprite _earthPower;
        [SerializeField] private Sprite _waterPower;

        private Transform _oldParent;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        protected override void Install()
        {
            _oldParent = _transform.parent;
            _transform.SetParent(_canvas.transform);

            var sprite = _windPower;

            switch (_powerType)
            {
                case PowerType.Wind:
                    sprite = _windPower;
                    break;
                case PowerType.Earth:
                    sprite = _earthPower;
                    break;
                case PowerType.Water:
                    sprite = _waterPower;
                    break;
            }

            
        }

        public override void ResetView()
        {
            if (_oldParent)
                _transform.SetParent(_oldParent);
        }
    }
}