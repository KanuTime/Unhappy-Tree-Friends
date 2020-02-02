using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public class TileGenerator : IGridEdit
    {
        [Inject] private int _width;
        [Inject] private int _height;

        public IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles
        {
            get
            {
                var result = new Dictionary<Vector2Int, EnvironmentType>();
                
                for (var x = 0; x < _width; x++)
                for (var y = 0; y < _height; y++)
                {
                    var env = (EnvironmentType) Random.Range(0, 5); 
                    result.Add(new Vector2Int(x, y), env);
                }
                
                return result;
            }
        }


    }
}