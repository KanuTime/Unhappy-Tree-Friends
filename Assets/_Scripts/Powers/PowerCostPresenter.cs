using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerCostPresenter : IInitializable
    {
        [Inject] private IManaData _data;
        [Inject] private Text _text;
        [Inject] private PowerType _power;
        
        public void Initialize()
        {
            _text.text = _data.CostsForPower(_power).ToString();
        }
    }
}