using _Scripts.Balance;
using _Scripts.Effects;
using _Scripts.Utility;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Music
{
    public class MusicController : Subscription
    {
        [Inject] private Slider _nature;
        [Inject] private Slider _human;

        [Inject] private IBalanceModel _balance;
        [Inject] private ISoundManager _soundManager;
       
        public override void Initialize()
        {
          // _soundManager.PlaySound(SoundType.MusicBase);
           // _soundManager.PlaySound(SoundType.MusicNature);
            //_soundManager.PlaySound(SoundType.MusicHuman);
        }

    }



}

