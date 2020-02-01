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
        float HumanGrowthDuration(HumanityDegree degree);
        float NatureGrowthDuration(NatureDegree degree);
    }
    
    [CreateAssetMenu(menuName = "Configs/Spread")]
    public class SpreadConfig : ScriptableObjectInstaller, ISpreadData
    {
        [SerializeField] private List<StartPoint> _spawnPoints;
        public IEnumerable<StartPoint> StartPoints => _spawnPoints;

        [SerializeField] private List<GrowthSetup> _humanGrowth;
        public float HumanGrowthDuration(HumanityDegree degree) => _humanGrowth.Single(entry => entry.Degree == (int) degree).TimeTilNextStage;

        [SerializeField] private List<GrowthSetup> _natureGrowth;
        public float NatureGrowthDuration(NatureDegree degree) => _natureGrowth.Single(entry => entry.Degree == (int) degree).TimeTilNextStage;
        
        public override void InstallBindings()
        {
            Container.Bind<ISpreadData>().FromInstance(this);
        }
    }
}