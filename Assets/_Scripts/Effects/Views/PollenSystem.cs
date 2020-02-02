using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class PollenSystem : Subscription
    {
        [Inject] private GameObject _gameObject;
        [Inject] private ITileModel _model;
        [Inject] private IViewFactory<PollenView> _pollenFactory;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.PollenExplosion)
                .Subscribe(_ => _pollenFactory.Create(_gameObject.transform.position))
                .AddTo(_disposer);
        }
    }
}