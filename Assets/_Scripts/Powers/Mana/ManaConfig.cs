using System;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public interface IManaData
    {
        float StartValue { get; }
        Vector2 StartDelay { get; }
        Vector2 TimeTilManaGrant { get; }
        float ManaGrantedPerTree { get; }
        float CostsForPower(PowerType type);
    }

    [CreateAssetMenu(menuName = "Configs/Mana")]
    public class ManaConfig : ScriptableObjectInstaller, IManaData
    {
        [SerializeField] private float _startValue;
        [SerializeField] private Vector2 _startDelay;
        [SerializeField] private Vector2 _timeTilManaGrant;
        [SerializeField] private float _manaGrantedPerTree;

        [SerializeField] private float _costsForEarth;
        [SerializeField] private float _costsForWind;
        [SerializeField] private float _costsForWater;

        public float StartValue => _startValue;
        public Vector2 StartDelay => _startDelay;
        public Vector2 TimeTilManaGrant => _timeTilManaGrant;
        public float ManaGrantedPerTree => _manaGrantedPerTree;

        public float CostsForPower(PowerType type)
        {
            switch (type)
            {
                case PowerType.None: return 0;
                case PowerType.Wind: return _costsForWind;
                case PowerType.Earth: return _costsForEarth;
                case PowerType.Water: return _costsForWater;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public override void InstallBindings()
        {
            Container.Bind<IManaData>().FromInstance(this);
        }
    }
}