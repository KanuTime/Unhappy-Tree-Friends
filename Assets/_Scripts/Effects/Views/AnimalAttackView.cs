using _Scripts.Effects;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class AnimalAttackView : View
    {
        [SerializeField] private float _lifeSpanInSeconds;

        [Inject] private ISoundManager _soundManager;
        
        protected override void Install()
        {
            Container.BindInterfacesTo<TimedDestructionSystem>().AsSingle().WithArguments(_lifeSpanInSeconds);
            Container.BindInterfacesTo<PlaySoundEffectSystem>().AsSingle().WithArguments(SoundType.PowerBees);
        }
    }
}