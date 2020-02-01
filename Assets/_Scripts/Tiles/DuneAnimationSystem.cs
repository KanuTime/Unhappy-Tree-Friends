using System;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.Tiles
{
    public class DuneAnimationSystem : Subscription
    {
        [Inject] private ParticleSystem _particle;
        [Inject] private Vector2 _timeBetweenDuneWinds;

        private SerialDisposable _disposable = new SerialDisposable();
        
        public override void Initialize()
        {
            var time = _timeBetweenDuneWinds.x + Random.value * (_timeBetweenDuneWinds.y - _timeBetweenDuneWinds.x);
            _disposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(_ =>
            {
                _particle.Play();
                Initialize();
            });
        }
    }
}