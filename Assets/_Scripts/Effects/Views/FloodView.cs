using _Scripts.Utility;
using UnityEngine;
using Zenject;
using _Scripts.Effects;


namespace DefaultNamespace
{
    public class FloodView : View
    {
        [SerializeField] private float _duration;
        [Inject] private ISoundManager _soundManager;

        
        protected override void Install()
        {
            Container.BindInterfacesTo<TimedDestructionSystem>().AsSingle().WithArguments(_duration);

            Container.BindInterfacesTo<PlaySoundEffectSystem>().AsSingle().WithArguments(SoundType.PowerFlood);
        }
    }
}