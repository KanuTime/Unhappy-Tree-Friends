using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public interface ISelectedPowerModel
    {
        IReactiveProperty<PowerType> SelectedPower { get; }
        void TriggerCooldown(PowerType powerType);
        bool IsAvailable(PowerType powerType);
        float Cooldown(PowerType powerType);
    }

    public class SelectedPowerModel : ISelectedPowerModel, IInitializable, ITickable
    {
        private float _earthCooldown;
        [Inject] private IPowerData _powerData;

        private float _waterCooldown;
        private float _windCooldown;

        public void Initialize()
        {
            _waterCooldown = 0;
            _earthCooldown = 0;
            _windCooldown = 0;
        }

        public IReactiveProperty<PowerType> SelectedPower { get; } = new ReactiveProperty<PowerType>(PowerType.None);

        public void TriggerCooldown(PowerType powerType)
        {
            switch (powerType)
            {
                case PowerType.Water:
                    _waterCooldown = _powerData.WaterCooldown;
                    break;
                case PowerType.Wind:
                    _windCooldown = _powerData.WindCooldown;
                    break;
                case PowerType.Earth:
                    _earthCooldown = _powerData.EarthCooldown;
                    break;
            }
        }

        public bool IsAvailable(PowerType powerType)
        {
            switch (powerType)
            {
                case PowerType.Water:
                    return _waterCooldown <= 0;
                case PowerType.Wind:
                    return _windCooldown <= 0;
                case PowerType.Earth:
                    return _earthCooldown <= 0;
            }

            return true;
        }

        public void Tick()
        {
            if (_waterCooldown > 0) _waterCooldown -= Time.deltaTime;
            if (_windCooldown > 0) _windCooldown -= Time.deltaTime;
            if (_earthCooldown > 0) _earthCooldown -= Time.deltaTime;
        }

        public float Cooldown(PowerType powerType)
        {
            switch (powerType)
            {
                case PowerType.Water:
                    return _waterCooldown;
                case PowerType.Wind:
                    return _windCooldown;
                case PowerType.Earth:
                    return _earthCooldown;
            }

            return 0;
        }
    }
}