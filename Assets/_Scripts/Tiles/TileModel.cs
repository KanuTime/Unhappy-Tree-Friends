using UniRx;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface ITileModel
    {
        Vector2Int Position { get; }
        IReadOnlyReactiveProperty<EnvironmentType> Type { get; }
    }
    
    public class TileModel
    {
        
    }
}