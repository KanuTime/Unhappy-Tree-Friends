using System.Collections.Generic;
using System.Linq;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    public class SoundEffects : MonoBehaviour, IInitializable
    {
        [Inject] private ISoundManager _soundManager;

        private readonly List<AudioSource> _audioSources = new List<AudioSource>();
        
        public void Initialize()
        {
            _soundManager.SoundEntryToPlay
                .Subscribe(Play)
                .AddTo(this);
        }

        private void Play(SoundEntry soundEntry)
        {
            var audioSource = GetAvailableAudioSource();
            audioSource.PlayOneShot(soundEntry.Audio, soundEntry.Volume);
        }
        
        private AudioSource GetAvailableAudioSource()
        {
            if (_audioSources.Any(source => !source.isPlaying))
            {
                return _audioSources.First(source => !source.isPlaying);
            }

            return ExpandSources();
        }

        private AudioSource ExpandSources()
        {
            var source = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(source);

            return source;
        }
    }

}