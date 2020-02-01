using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class FireSystem : Subscription
    {
        [Inject] private GameObject _gameObject;
        [Inject] private IViewFactory<FireView> _factory;
        [Inject] private ITileModel _model;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.Fire)
                .Subscribe(_ => _factory.Create(_gameObject.transform.position))
                .AddTo(_disposer);
        }
    }
}