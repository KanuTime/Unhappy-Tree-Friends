using _Scripts.Utility;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Powers
{
    public class ManaPresenter : Subscription
    {
        [Inject] private Text _displayText;
        [Inject] private IManaModel _model;
        
        public override void Initialize()
        {
            _model.Mana.Subscribe(Display).AddTo(_disposer);
        }

        private void Display(float mana)
        {
            _displayText.text = mana.ToString("F0");
        }
    }
}