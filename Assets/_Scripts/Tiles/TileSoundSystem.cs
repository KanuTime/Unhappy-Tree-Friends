using _Scripts.Effects;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Tiles
{
    public class TileSoundSystem : Subscription
    {
        [Inject] private ITileModel _model;
        [Inject] private ISoundManager _soundManager;
        
        public override void Initialize()
        {
            _model.Type.Pairwise().Where(pair => pair.Current != pair.Previous)
                .Subscribe(PlaySounds).AddTo(_disposer);
        }

        private void PlaySounds(Pair<EnvironmentType> pair)
        {
            switch (pair.Current)
            {
                case EnvironmentType.Desert:
                    _soundManager.PlaySound(SoundType.CreateDesert);
                    break;
                case EnvironmentType.Grassland:
                    _soundManager.PlaySound(SoundType.CreateGrass);
                    break;
                case EnvironmentType.Mountain:
                    _soundManager.PlaySound(SoundType.CreateMountain);
                    break;
                case EnvironmentType.Sea:
                    _soundManager.PlaySound(SoundType.CreateWater);
                    break;
                case EnvironmentType.Swamp:
                    _soundManager.PlaySound(SoundType.CreateSwamp);
                    break;
            }
        }
    }
}