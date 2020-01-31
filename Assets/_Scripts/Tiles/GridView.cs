using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Tiles
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private Transform _bottomLeftCorner;

        private void OnValidate()
        {
            var minCol = _bottomLeftCorner.position.x;
            var minRow = _bottomLeftCorner.position.z;

            var dictionary = new Dictionary<Index, TileView>();

            var children = GetComponentsInChildren<TileView>();
            foreach (var child in children)
            {
                var col = Mathf.FloorToInt(child.transform.position.x - minCol);
                var row = Mathf.FloorToInt(child.transform.position.z - minRow);
                
                dictionary.Add(new Index(row, col), child);
            }
            
            Debug.Log($"Dictionary contains {dictionary.Count} elements");
        }
    }
}