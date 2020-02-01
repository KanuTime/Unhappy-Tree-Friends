using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class FloodSystem : Subscription
    {
        [Inject] private GameObject _gameObject;
        [Inject] private IViewFactory<FloodView> _factory;
        [Inject] private ITileModel _model;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.Flood)
                .Subscribe(_ => _factory.Create(_gameObject.transform.position))
                .AddTo(_disposer);
        }
    }
}