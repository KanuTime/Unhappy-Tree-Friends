using _Scripts.Utility;
using _Scripts.Effects;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class FireView : View
    {
        [SerializeField] private float _duration;
        
        [Inject] private ISoundManager _soundManager;
        
        protected override void Install()
        {
            Container.BindInterfacesTo<TimedDestructionSystem>().AsSingle().WithArguments(_duration);
        Container.BindInterfacesTo<PlaySoundEffectSystem>().AsSingle().WithArguments(SoundType.PowerFire);
        }
    }
}
