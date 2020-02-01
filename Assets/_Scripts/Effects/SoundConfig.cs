using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Effects
{
    [Serializable]
    public class SoundEntry
    {
        public SoundType Type;
        public AudioClip Audio;
        public float Volume = 1;
    }

    public enum SoundType
    {
        CreateCity,
        CreateDesert,
        CreateGrass,
        CreateMountain,
        CreatePioneer,
        CreateSwamp,
        CreateTree,
        CreateVillage,
        CreateWater,
        
        DestroyHumans,
        DestroyNature,
        
        UiSelectTile,
        
        PowerBees,
        PowerEarthquake,
        PowerFlood,
        PowerTornado,
        
        HouseAppear,
        HouseDisappear
    }

    public interface ISoundManager
    {
        UniRx.IObservable<SoundEntry> SoundEntryToPlay { get; }
        void PlaySound(SoundType type);
    }

    [CreateAssetMenu(menuName = "Configs/Sound")]
    public class SoundConfig : ScriptableObjectInstaller, ISoundManager
    {
        private readonly Subject<SoundEntry> _soundEntryToPlay = new Subject<SoundEntry>();
        public UniRx.IObservable<SoundEntry> SoundEntryToPlay => _soundEntryToPlay;

        [SerializeField] private List<SoundEntry> _entries;
        
        public void PlaySound(SoundType type)
        {
            var foundEntry = _entries.First(entry => entry.Type == type);
            _soundEntryToPlay.OnNext(foundEntry);
        }

        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>().FromInstance(this);
        }
    }
}
