using _Scripts.Factions;
using UniRx;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface ITileModel
    {
        Vector2Int Position { get; }
        IReactiveProperty<EnvironmentType> Type { get; }
        IReactiveProperty<int> Humanity { get; }
        IReactiveProperty<int> Nature { get; }
    }
    
    public class TileModel : ITileModel
    {
        public Vector2Int Position { get; }
        public IReactiveProperty<EnvironmentType> Type { get; } = new ReactiveProperty<EnvironmentType>();
        public IReactiveProperty<int> Humanity { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> Nature { get; } = new ReactiveProperty<int>();

        public TileModel(Vector2Int position, EnvironmentType type, int humans, int nature)
        {
            Position = position;
            Type.Value = type;
            Humanity.Value = humans;
            Nature.Value = nature;
        }
    }
}