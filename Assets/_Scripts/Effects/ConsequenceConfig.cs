using System;
using System.Collections.Generic;
using _Scripts.Powers;
using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    [Serializable]
    public class ConsequenceSetup
    {
        public ConsequenceType _consequence;
        public EnvironmentType _environment;
        public PowerType _power;
    }

    public interface IConsequenceData
    {
        IReadOnlyList<ConsequenceSetup> Consequences { get; }
    }

    [CreateAssetMenu(menuName = "Configs/Consequences")]
    public class ConsequenceConfig : ScriptableObjectInstaller, IConsequenceData
    {
        [SerializeField] private List<ConsequenceSetup> _consequences;
        public IReadOnlyList<ConsequenceSetup> Consequences => _consequences;

        public override void InstallBindings()
        {
            Container.Bind<IConsequenceData>().FromInstance(this);
        }
    }
}