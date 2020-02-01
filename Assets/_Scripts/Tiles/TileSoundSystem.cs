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
                    _soundManager.TileCreateDesert();
                    break;
                case EnvironmentType.Grassland:
                    _soundManager.TileCreateGrass();
                    break;
                case EnvironmentType.Mountain:
                    _soundManager.TileCreateMountain();
                    break;
                case EnvironmentType.Sea:
                    _soundManager.TileCreateWater();
                    break;
                case EnvironmentType.Swamp:
                    _soundManager.TileCreateSwamp();
                    break;
                default:
                    break;
            }
        }
    }
}