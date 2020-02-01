using _Scripts.Factions;
using UniRx;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface ITileModel
    {
        Vector2Int Position { get; }
        IReactiveProperty<EnvironmentType> Type { get; }
        IReactiveProperty<HumanityDegree> Humanity { get; }
        IReactiveProperty<NatureDegree> Nature { get; }
    }
    
    public class TileModel : ITileModel
    {
        public Vector2Int Position { get; }
        public IReactiveProperty<EnvironmentType> Type { get; } = new ReactiveProperty<EnvironmentType>();
        public IReactiveProperty<HumanityDegree> Humanity { get; } = new ReactiveProperty<HumanityDegree>(HumanityDegree._0);
        public IReactiveProperty<NatureDegree> Nature { get; } = new ReactiveProperty<NatureDegree>(NatureDegree._0);

        public TileModel(Vector2Int position, EnvironmentType type)
        {
            Position = position;
            Type.Value = type;
        }
    }
}