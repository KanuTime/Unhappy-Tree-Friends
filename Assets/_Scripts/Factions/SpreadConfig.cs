using System;
using System.Collections.Generic;
using System.Linq;
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

    public interface ISpreadData
    {
        IEnumerable<StartPoint> StartPoints { get; }
        float GrowthDuration(Faction faction, int degree);
    }
    
    [CreateAssetMenu(menuName = "Configs/Spread")]
    public class SpreadConfig : ScriptableObjectInstaller, ISpreadData
    {
        [SerializeField] private List<StartPoint> _spawnPoints;
        public IEnumerable<StartPoint> StartPoints => _spawnPoints;

        [SerializeField] private List<GrowthSetup> _humanGrowth;
        [SerializeField] private List<GrowthSetup> _natureGrowth;
        
        public float GrowthDuration(Faction faction, int degree)
        {
            return (faction == Faction.Humans ? _humanGrowth : _natureGrowth).Single(entry => entry.Degree == degree).TimeTilNextStage;
        }

        public override void InstallBindings()
        {
            Container.Bind<ISpreadData>().FromInstance(this);
        }
    }
}