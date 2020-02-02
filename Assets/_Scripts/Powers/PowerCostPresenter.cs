using _Scripts.Utility;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerCostPresenter : Subscription
    {
        [Inject] private IManaData _data;
        [Inject] private Text _text;
        [Inject] private PowerType _power;
        [Inject] private IManaModel _mana;
        
        public override void Initialize()
        {
            _text.text = _data.CostsForPower(_power).ToString();

            _mana.Mana.Select(ColorButton).DistinctUntilChanged().Subscribe(ChangeColor).AddTo(_disposer);
        }

        private Color ColorButton(float mana)
        {
            return mana >= _data.CostsForPower(_power) ? Color.black : Color.red;
        }

        private void ChangeColor(Color color)
        {
            _text.color = color;
        }
    }
}