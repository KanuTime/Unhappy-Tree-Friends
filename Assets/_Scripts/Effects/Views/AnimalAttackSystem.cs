using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class AnimalAttackSystem : Subscription
    {
        [Inject] private GameObject _gameObject;
        [Inject] private ITileModel _model;
        [Inject] private IViewFactory<AnimalAttackView> _viewFactory;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.AnimalSwarm)
                .Subscribe(_ => _viewFactory.Create(_gameObject.transform.position))
                .AddTo(_disposer);
        }
    }
}