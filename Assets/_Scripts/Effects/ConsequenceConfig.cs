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
        public EnvironmentType _environment;
        public PowerType _power;
        public ConsequenceType _consequence;
    }
    
    [CreateAssetMenu(menuName = "Configs/Consequences")]
    public class ConsequenceConfig : ScriptableObjectInstaller
    {
        [SerializeField] private List<ConsequenceSetup> _consequences;
        
        public override void InstallBindings()
        {
            
        }
    }
}