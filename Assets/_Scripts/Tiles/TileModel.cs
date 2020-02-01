using UniRx;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface ITileModel
    {
        Vector2Int Position { get; }
        IReadOnlyReactiveProperty<EnvironmentType> Type { get; }
    }
    
    public class TileModel : ITileModel
    {
        public Vector2Int Position { get; }

        private readonly IReactiveProperty<EnvironmentType> _type = new ReactiveProperty<EnvironmentType>();
        public IReadOnlyReactiveProperty<EnvironmentType> Type => _type;

        public TileModel(Vector2Int position, EnvironmentType type)
        {
            Position = position;
            _type.Value = type;
        }
    }
}