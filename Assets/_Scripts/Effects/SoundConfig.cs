using System;
using UniRx;
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
        UniRx.IObservable<SoundEntry> SoundEntryToPlay { get; }
        void TileCreateCity();
        void TileCreateDesert();
        void TileCreateGrass();
        void TileCreateMountain();
        void TileCreatePioneer();
        void TileCreateSwamp();
        void TileCreateTree();
        void TileCreateVillage();
        void TileCreateWater();
        void TileDestroyHumans();
        void TileDestroyNature();
        void UISelectTile();
        
        // NEW
         void Power_Bees();
         void Power_Earthquake();
         void Power_Flood();
         void Power_Tornado();
        
        
    }

    [CreateAssetMenu(menuName = "Configs/Sound")]
    public class SoundConfig : ScriptableObjectInstaller, ISoundManager
    {
        private Subject<SoundEntry> _soundEntryToPlay = new Subject<SoundEntry>();
        public UniRx.IObservable<SoundEntry> SoundEntryToPlay => _soundEntryToPlay;

        [SerializeField] private SoundEntry _TileCreateCity;
        [SerializeField] private SoundEntry _TileCreateDesert;
        [SerializeField] private SoundEntry _TileCreateGrass;
        [SerializeField] private SoundEntry _TileCreateMountain;
        [SerializeField] private SoundEntry _TileCreatePioneer;
        [SerializeField] private SoundEntry _TileCreateSwamp;
        [SerializeField] private SoundEntry _TileCreateTree;
        [SerializeField] private SoundEntry _TileCreateVillage;
        [SerializeField] private SoundEntry _TileCreateWater;
        [SerializeField] private SoundEntry _TileDestroyHumans;
        [SerializeField] private SoundEntry _TileDestroyNature;
        [SerializeField] private SoundEntry _UISelectTile;
        
        // NEW
        [SerializeField] private SoundEntry _Power_Bees;
        [SerializeField] private SoundEntry _Power_Earthquake;
        [SerializeField] private SoundEntry _Power_Flood;
        [SerializeField] private SoundEntry _Power_Tornado;
        
        
        public void TileCreateCity() => PlaySound(_TileCreateCity);
        public void TileCreateDesert() => PlaySound(_TileCreateDesert);
        public void TileCreateGrass() => PlaySound(_TileCreateGrass);
        public void TileCreateMountain() => PlaySound(_TileCreateMountain);
        public void TileCreatePioneer() => PlaySound(_TileCreatePioneer);
        public void TileCreateSwamp() => PlaySound(_TileCreateSwamp);
        public void TileCreateTree() => PlaySound(_TileCreateTree);
        public void TileCreateVillage() => PlaySound(_TileCreateVillage);
        public void TileCreateWater() => PlaySound(_TileCreateWater);
        public void TileDestroyHumans() => PlaySound(_TileDestroyHumans);
        public void TileDestroyNature() => PlaySound(_TileDestroyNature);
        public void UISelectTile() => PlaySound(_UISelectTile);
        
        // NEW
        public void Power_Bees() => PlaySound(_Power_Bees);
        public void Power_Earthquake() => PlaySound(_Power_Earthquake);
        public void Power_Flood() => PlaySound(_Power_Flood);
        public void Power_Tornado() => PlaySound(_Power_Tornado);
        

        private void PlaySound(SoundEntry entry)
        {
            _soundEntryToPlay.OnNext(entry);
        }


        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>().FromInstance(this);
        }
    }
}
