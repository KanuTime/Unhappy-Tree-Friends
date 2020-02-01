using System.Linq;
using _Scripts.Factions;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public interface IGridModel
    {
        
    }
    
    public class GridModel : IGridModel, IInitializable
    {
        private const int K_offset = 5;
        
        [Inject] private IGridEdit _gridEdit;
        [Inject] private IViewFactory<TileView> _viewFactory;
        [Inject] private ISpreadData _spreadData;
        
        public void Initialize()
        {
            var tiles = _gridEdit.Tiles;
            foreach (var tile in tiles)
            {
                var startPoints = _spreadData.StartPoints.Where(point => point.Position == tile.Key).ToList();
                var humanityDegree = startPoints.Where(point => point.Faction == Faction.Humans).Sum(point => point.Intensity); 
                var natureDegree = startPoints.Where(point => point.Faction == Faction.Nature).Sum(point => point.Intensity); 
                
                var tileModel = new TileModel(tile.Key, tile.Value, humanityDegree, natureDegree);
                _viewFactory.Create(new Vector3(tile.Key.x - K_offset, 0, tile.Key.y - K_offset), tileModel);
            }
        }
    }
}