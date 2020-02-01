using _Scripts.Utility;
using UniRx;
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
        
        public void Initialize()
        {
            var tiles = _gridEdit.Tiles;
            foreach (var tile in tiles)
            {
                var tileModel = new TileModel(tile.Key, tile.Value);
                _viewFactory.Create(new Vector3(tile.Key.x - K_offset, 0, tile.Key.y - K_offset), tileModel);
            }
        }
    }
}