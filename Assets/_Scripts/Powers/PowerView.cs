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

        private Transform _oldParent;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        protected override void Install()
        {
            _oldParent = _transform.parent;
            _transform.parent = _canvas.transform;

            Container.BindInterfacesTo<PowerController>().AsSingle().WithArguments(_button, _powerType, _text);
        }

        public override void ResetView()
        {
            if (_oldParent)
                _transform.parent = _oldParent;
        }
    }
}