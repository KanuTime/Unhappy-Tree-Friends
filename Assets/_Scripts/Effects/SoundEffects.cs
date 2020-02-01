using System.Collections.Generic;
using System.Linq;
using _Scripts.Utility;
using UnityEngine;

namespace _Scripts.Effects
{
    public class SoundEffects : View
    {
        private List<AudioSource> _audioSources;

        protected override void Install()
        {
            
        }
        
        private AudioSource GetAvailableAudioSource()
        {
            if (_audioSources.Any(source => !source.isPlaying))
            {
                return _audioSources.First(source => !source.isPlaying);
            }
            else
            {
                return ExpandSources();
            }	
        }

        private AudioSource ExpandSources()
        {
            var source = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(source);

            return source;
        }

    }

}