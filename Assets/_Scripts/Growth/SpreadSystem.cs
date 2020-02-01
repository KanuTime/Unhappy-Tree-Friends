using System.Linq;
using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Growth
{
    public class SpreadSystem : Subscription, ITickable
    {
        [Inject] private ISpread _spread;
        [Inject] private Faction _faction;
        [Inject] private ISpreadData _data;
        [Inject] private IGridModel _grid;
        [Inject] private ITileModel _model;
        
        private float _spreadIntensity;
        
        public void Tick()
        {
            if (_spreadIntensity >= _data.SpreadMaximum)
                return;
            
            var baseValue = _data.EnvironmentSpreadBase(_faction, _model.Type.Value);
            var factor = _grid.AdjacentTiles(_model.Position)
                .Sum(adjacent => _data.AllySpreadInfluence(_faction, adjacent.Intensity(_faction).Value));

            _spreadIntensity += baseValue * factor * Time.deltaTime;
            
            if (_spreadIntensity >= _data.SpreadMaximum)
                _spread.Triggered.OnNext(_faction);
        }

        public override void Initialize()
        {
            _model.Intensity(_faction).Where(value => value == 0)
                .Subscribe(_ => _spreadIntensity = 0).AddTo(_disposer);
        }
    }
}