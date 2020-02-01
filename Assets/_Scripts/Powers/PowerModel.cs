using System;
using UniRx;

namespace _Scripts.Powers
{
    public interface IPowerModel
    {
        IReactiveProperty<PowerType> SelectedPower { get; }
    }

    public class PowerModel: IPowerModel
    {
        public IReactiveProperty<PowerType> SelectedPower { get; } = new ReactiveProperty<PowerType>();

        public PowerModel()
        {
        }
    }
}