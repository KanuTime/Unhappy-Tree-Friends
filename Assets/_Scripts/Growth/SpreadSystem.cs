using _Scripts.Factions;
using _Scripts.Tiles;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Growth
{
    public interface ISpreadSystem
    {
        IObservable<Faction> SpreadingStarted { get; }
    }
    
    public class SpreadSystem : ITickable, ISpreadSystem
    {
        [Inject] private Faction _faction;
        [Inject] private ISpreadData _data;
        [Inject] private IGridModel _grid;
        [Inject] private ITileModel _model;
        
        private float _spreadIntensity;
        
        private readonly Subject<Faction> _spreadingStarted = new Subject<Faction>();
        public IObservable<Faction> SpreadingStarted => _spreadingStarted;
        
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
                _spreadingStarted.OnNext(_faction);
        }
    }
}