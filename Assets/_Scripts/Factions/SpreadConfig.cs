using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts.Factions
{
    [Serializable]
    public class StartPoint
    {
        public Faction Faction;
        public Vector2Int Position;
        public int Intensity;
    }

    [Serializable]
    public class GrowthSetup
    {
        public int Degree;
        public float TimeTilNextStage;
    }

    [Serializable]
    public class SpreadSetup
    {
        public EnvironmentType OwnEnvironment;
        public int AllyIntensity;
        public float SpreadIncreasePerSecond;
    }

    public interface ISpreadData
    {
        IEnumerable<StartPoint> StartPoints { get; }
        float GrowthDuration(Faction faction, int degree);
        float SpreadIncreasePerSecond(Faction faction, EnvironmentType environment, int intensity);
    }
    
    [CreateAssetMenu(menuName = "Configs/Spread")]
    public class SpreadConfig : ScriptableObjectInstaller, ISpreadData
    {
        [SerializeField] private List<StartPoint> _spawnPoints;
        public IEnumerable<StartPoint> StartPoints => _spawnPoints;

        [SerializeField] private List<GrowthSetup> _humanGrowth;
        [SerializeField] private List<GrowthSetup> _natureGrowth;

        [SerializeField] private List<SpreadSetup> _humanSpread;
        [SerializeField] private List<SpreadSetup> _natureSpread;
        
        public float GrowthDuration(Faction faction, int degree)
        {
            return (faction == Faction.Humans ? _humanGrowth : _natureGrowth).Single(entry => entry.Degree == degree).TimeTilNextStage;
        }
        
        public float SpreadIncreasePerSecond(Faction faction, EnvironmentType environment, int allyIntensity)
        {
            var setup = (faction == Faction.Humans ? _humanSpread : _natureSpread)
                .SingleOrDefault(entry => entry.OwnEnvironment == environment && entry.AllyIntensity == allyIntensity);
            return setup?.SpreadIncreasePerSecond ?? 0f;
        }

        public override void InstallBindings()
        {
            Container.Bind<ISpreadData>().FromInstance(this);
        }
    }
}