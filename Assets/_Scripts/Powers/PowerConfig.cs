using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{

    [CreateAssetMenu(menuName = "Configs/Powers")]
    public class PowerConfig : ScriptableObjectInstaller, IPowerData
    {
        [SerializeField] private float _earthCooldown;
        [SerializeField] private float _waterCooldown;
        [SerializeField] private float _windCooldown;

        public float WindCooldown => _windCooldown;

        public float EarthCooldown => _earthCooldown;

        public float WaterCooldown => _waterCooldown;

        public float Cooldown(PowerType type)
        {
            switch (type)
            {
                case PowerType.Water:
                    return WaterCooldown;
                case PowerType.Wind:
                    return WindCooldown;
                case PowerType.Earth:
                    return EarthCooldown;
                default:
                    return 0;
            }
        }

        public override void InstallBindings()
        {
            Container.Bind<IPowerData>().FromInstance(this);
        }
    }
}