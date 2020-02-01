using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface IGrid
    {
        IReadOnlyDictionary<Vector2Int, TileView> Tiles { get; }
    }
    
    public class GridView : MonoBehaviour, IGrid
    {
        [SerializeField] private Transform _bottomLeftCorner;

        private Dictionary<Vector2Int, TileView> _tiles;
        public IReadOnlyDictionary<Vector2Int, TileView> Tiles
        {
            get
            {
                if (_tiles != null)
                    return _tiles;
                
                _tiles = new Dictionary<Vector2Int, TileView>();
                
                var minCol = _bottomLeftCorner.position.x;
                var minRow = _bottomLeftCorner.position.z;

                var children = GetComponentsInChildren<TileView>();
                foreach (var child in children)
                {
                    var col = Mathf.FloorToInt(child.transform.position.x - minCol);
                    var row = Mathf.FloorToInt(child.transform.position.z - minRow);
                    var position = new Vector2Int(row, col);
                
                    _tiles.Add(position, child);
                    child.Position = position;
                }

                return _tiles;
            }
        } 
    }
}