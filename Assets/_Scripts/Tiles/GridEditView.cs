using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface IGridEdit
    {
        IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles { get; }
    }
    
    public class GridEditView : MonoBehaviour, IGridEdit
    {
        [SerializeField] private Transform _bottomLeftCorner;

        private Dictionary<Vector2Int, EnvironmentType> _tiles;
        public IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles
        {
            get
            {
                if (_tiles != null)
                    return _tiles;
                
                _tiles = new Dictionary<Vector2Int, EnvironmentType>();
                
                var minCol = _bottomLeftCorner.position.x;
                var minRow = _bottomLeftCorner.position.z;

                var children = GetComponentsInChildren<TileEditView>();
                foreach (var child in children)
                {
                    var col = Mathf.FloorToInt(child.transform.position.x - minCol);
                    var row = Mathf.FloorToInt(child.transform.position.z - minRow);
                    var position = new Vector2Int(row, col);
                
                    _tiles.Add(position, child.Type);
                }

                return _tiles;
            }
        } 
    }
}