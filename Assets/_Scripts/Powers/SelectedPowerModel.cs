using System;
using UniRx;

namespace _Scripts.Powers
{
    public interface ISelectedPowerModel
    {
        IReactiveProperty<PowerType> SelectedPower { get; }
    }

    public class SelectedPowerModel: ISelectedPowerModel
    {
        public IReactiveProperty<PowerType> SelectedPower { get; } = new ReactiveProperty<PowerType>(PowerType.None);

        public SelectedPowerModel()
        {
        }
    }
}