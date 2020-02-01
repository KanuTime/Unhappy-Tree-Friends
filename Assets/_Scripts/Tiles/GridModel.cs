using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public interface IGridModel
    {
        IObservable<ITileModel> ClickedOnTile { get; }
    }
    
    public class GridModel : IGridModel, IInitializable
    {
        private const int K_offset = 5;
        
        [Inject] private IGridEdit _gridEdit;
        [Inject] private IViewFactory<TileView> _viewFactory;
        
        private readonly ISubject<ITileModel> _clickedOnTile = new Subject<ITileModel>();
        public IObservable<ITileModel> ClickedOnTile => _clickedOnTile;
        
        public void Initialize()
        {
            var tiles = _gridEdit.Tiles;
            foreach (var tile in tiles)
            {
                _viewFactory.Create(new Vector3(tile.Key.x - K_offset, 0, tile.Key.y - K_offset), tile.Value);
            }
        }
    }
}