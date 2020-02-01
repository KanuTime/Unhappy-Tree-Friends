using _Scripts.Utility;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Balance
{
    public class BalancePresenter : Subscription
    {
        [Inject] private Slider _nature;
        [Inject] private Slider _human;

        [Inject] private IBalanceModel _balance;

        public override void Initialize()
        {
            _balance.NatureIntensity.Subscribe(value => _nature.value = 1f * value / _balance.MaxAmount).AddTo(_disposer);
            _balance.HumansIntensity.Subscribe(value => _human.value = 1f * value / _balance.MaxAmount).AddTo(_disposer);
        }
    }
}