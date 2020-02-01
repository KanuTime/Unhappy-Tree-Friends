using System;
using _Scripts.Effects;
using Zenject;

namespace DefaultNamespace
{
    public class PlaySoundEffectSystem : IInitializable
    {
        [Inject] private ISoundManager _soundManager;
        [Inject] private SoundType _sound;
        
        public void Initialize()
        {
            _soundManager.PlaySound(_sound);
        }
    }
}