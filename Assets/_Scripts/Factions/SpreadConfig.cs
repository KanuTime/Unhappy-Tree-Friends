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
    public class AllySpreadInfluence
    {
        public int AllyIntensity;
        public float SpreadFactor;
    }
    
    [Serializable]
    public class EnvironmentSpreadFactor
    {
        public EnvironmentType Environment;
        public float SpreadBase;
    }

    public interface ISpreadData
    {
        IEnumerable<StartPoint> StartPoints { get; }
        float SpreadMaximum { get; }

        float GrowthDuration(Faction faction, int degree);
        float AllySpreadInfluence(Faction faction, int intensity);
        float EnvironmentSpreadBase(Faction faction, EnvironmentType environment);
    }
    
    [CreateAssetMenu(menuName = "Configs/Spread")]
    public class SpreadConfig : ScriptableObjectInstaller, ISpreadData
    {
        [SerializeField] private List<StartPoint> _spawnPoints;
        public IEnumerable<StartPoint> StartPoints => _spawnPoints;

        [SerializeField] private float _spreadMaximum = 100;
        public float SpreadMaximum => _spreadMaximum;
        
        [Header("Humans")]
        [SerializeField] private List<GrowthSetup> _humanGrowth;
        [SerializeField] private List<AllySpreadInfluence> _humanAllyInfluence;
        [SerializeField] private List<EnvironmentSpreadFactor> _humanEnvironmentBase;
        
        [Header("Nature")]
        [SerializeField] private List<GrowthSetup> _natureGrowth;
        [SerializeField] private List<AllySpreadInfluence> _natureAllyInfluence;
        [SerializeField] private List<EnvironmentSpreadFactor> _natureEnvironmentBase;
        
        public float GrowthDuration(Faction faction, int degree)
        {
            return (faction == Faction.Humans ? _humanGrowth : _natureGrowth).Single(entry => entry.Degree == degree).TimeTilNextStage;
        }
        
        public float AllySpreadInfluence(Faction faction, int allyIntensity)
        {
            var setup = (faction == Faction.Humans ? _humanAllyInfluence : _natureAllyInfluence)
                .SingleOrDefault(entry => entry.AllyIntensity == allyIntensity);
            return setup?.SpreadFactor ?? 0f;
        }

        public float EnvironmentSpreadBase(Faction faction, EnvironmentType environment)
        {
            var setup = (faction == Faction.Humans ? _humanEnvironmentBase : _natureEnvironmentBase)
                .SingleOrDefault(entry => entry.Environment == environment);
            return setup?.SpreadBase ?? 0f;
        }

        public override void InstallBindings()
        {
            Container.Bind<ISpreadData>().FromInstance(this);
        }
    }
}