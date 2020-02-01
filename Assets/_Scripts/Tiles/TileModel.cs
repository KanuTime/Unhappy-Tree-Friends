using _Scripts.Factions;
using UniRx;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface ITileModel
    {
        Vector2Int Position { get; }
        IReactiveProperty<EnvironmentType> Type { get; }
        IReactiveProperty<int> Intensity(Faction faction);
    }
    
    public class TileModel : ITileModel
    {
        public Vector2Int Position { get; }
        public IReactiveProperty<EnvironmentType> Type { get; } = new ReactiveProperty<EnvironmentType>();
        private readonly IReactiveProperty<int> _humanity = new ReactiveProperty<int>();
        private readonly IReactiveProperty<int> _nature = new ReactiveProperty<int>();
        public IReactiveProperty<int> Intensity(Faction faction) => faction == Faction.Humans ? _humanity : _nature;

        public TileModel(Vector2Int position, EnvironmentType type, int humans, int nature)
        {
            Position = position;
            Type.Value = type;
            _humanity.Value = humans;
            _nature.Value = nature;
        }
    }
}