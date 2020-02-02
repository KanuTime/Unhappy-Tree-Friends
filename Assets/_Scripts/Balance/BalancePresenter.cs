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
            _balance.HumansIntensity.CombineLatest(_balance.NatureIntensity, (h, n) => 1f * h / (h + n))
                .Subscribe(value =>
                {
                    _human.value = value;
                    _nature.value = 1 - value;
                }).AddTo(_disposer);
        }
    }
}