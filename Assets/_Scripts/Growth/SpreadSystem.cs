using _Scripts.Factions;
using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts.Growth
{
    public class SpreadSystem : ITickable
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
            
            foreach (var adjacent in _grid.AdjacentTiles(_model.Position))
            {
                var baseValue = _data.EnvironmentSpreadBase(_faction, adjacent.Type.Value);
                var factor = _data.AllySpreadInfluence(_faction, adjacent.Intensity(_faction).Value);
                _spreadIntensity += baseValue * factor * Time.deltaTime;
            }
            
            if (_spreadIntensity >= _data.SpreadMaximum)
                _spread.Triggered.OnNext(_faction);
        }
    }
}