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
            if (_spreadIntensity >= 100f)
                return;
            
            foreach (var adjacent in _grid.AdjacentTiles(_model.Position))
            {
                if (adjacent.Intensity(_faction).Value <= 0)
                    return;
                
                var spread = _data.SpreadIncreasePerSecond(_faction, _model.Type.Value, _model.Intensity(_faction).Value);
                _spreadIntensity += spread * Time.deltaTime;
            }
            
            if (_spreadIntensity >= 100f)
                _spreadingStarted.OnNext(_faction);
        }
    }
}