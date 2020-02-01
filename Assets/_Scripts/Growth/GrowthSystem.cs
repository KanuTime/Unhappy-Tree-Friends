using System;
using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Growth
{
    public class GrowthSystem : Subscription
    {
        [Inject] private ITileModel _model;
        [Inject] private ISpreadData _data;
        
        public override void Initialize()
        {
            _model.Humanity.Subscribe(StartHumanGrowth).AddTo(_disposer);
            _model.Nature.Subscribe(StartNatureGrowth).AddTo(_disposer);
        }

        private void StartHumanGrowth(HumanityDegree degree)
        {
            if (degree == HumanityDegree._0)
                return;

            var timeTilNextStage = _data.HumanGrowthDuration(degree);
            Observable.Timer(TimeSpan.FromSeconds(timeTilNextStage))
                .Where(_ => _model.Humanity.Value == degree).Take(1)
                .Subscribe(_ => _model.Humanity.Value++).AddTo(_disposer);
        }
        
        private void StartNatureGrowth(NatureDegree degree)
        {
            if (degree == NatureDegree._0)
                return;

            var timeTilNextStage = _data.NatureGrowthDuration(degree);
            Observable.Timer(TimeSpan.FromSeconds(timeTilNextStage))
                .Where(_ => _model.Nature.Value == degree).Take(1)
                .Subscribe(_ => _model.Nature.Value++).AddTo(_disposer);
        }
    }
}