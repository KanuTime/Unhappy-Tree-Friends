using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class MousePositionLogger : Subscription
    {
        [Inject] private IMousePositionController _mousePosition;
        
        public override void Initialize()
        {
            _mousePosition.Clicked.Subscribe(model => Debug.Log($"{model.Position} {model.Type}")).AddTo(_disposer);
        }
    }
}