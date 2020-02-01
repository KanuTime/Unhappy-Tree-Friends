using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class TornadoSystem : Subscription
    {
        [Inject] private GameObject _gameObject;
        [Inject] private ITileModel _model;
        [Inject] private IViewFactory<TornadoView> _tornadoFactory;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.Hurricane)
                .Subscribe(_ => _tornadoFactory.Create(_gameObject.transform.position))
                .AddTo(_disposer);
        }
    }
}