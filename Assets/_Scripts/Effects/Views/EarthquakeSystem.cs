using System;
using _Scripts.Effects;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EarthquakeSystem : Subscription
    {
        [Inject] private float _duration;
        [Inject] private Animator _animator;
        [Inject] private ITileModel _model;
        
        public override void Initialize()
        {
            _model.Consequence
                .Where(type => type == ConsequenceType.Earthquake)
                .Subscribe(ActivateEffect)
                .AddTo(_disposer);
        }

        private void ActivateEffect()
        {
            _animator.enabled = true;
            Observable.Timer(TimeSpan.FromSeconds(_duration))
                .Where(_ => _animator)
                .Subscribe(_ => _animator.enabled = false);
        }
    }
}