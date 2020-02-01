using System;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    [Serializable]
    public class SoundEntry
    {
        public AudioClip Audio;
        public float Volume = 1;
    }

    public interface ISoundManager
    {
        void PlayTestSound();
        void PlayTestSound2();
    }
    
    [CreateAssetMenu(menuName = "Configs/Sound")]
    public class SoundConfig : ScriptableObjectInstaller, ISoundManager
    {
        [SerializeField] private SoundEntry _testSound;
        [SerializeField] private SoundEntry _testSound2;

        public void PlayTestSound() => PlaySound(_testSound);
        public void PlayTestSound2() => PlaySound(_testSound2);
        
        private void PlaySound(SoundEntry entry)
        {
            // TODO
        }

        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>().FromInstance(this);
        }
    }
}