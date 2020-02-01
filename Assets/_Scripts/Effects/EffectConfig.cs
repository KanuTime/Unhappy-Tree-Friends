using System;
using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public enum EffectType
    {
        EnvironmentChange,
        KillTree,
        KillHuman
    }

    [Serializable]
    public class EffectData
    {
        public EffectType Effect;
        public EnvironmentType Environment;
        public int Intensity;
    }

    [Serializable]
    public class EffectSetup
    {
        public EnvironmentType Environment;
        public ConsequenceType ConsequenceType;
        public List<EffectData> Effects;
    }

    [CreateAssetMenu(menuName = "Configs/Effects")]
    public class EffectConfig : ScriptableObjectInstaller
    {
        [SerializeField] private List<EffectSetup> _effects;

        public override void InstallBindings()
        {

        }
    }
}