using _Scripts.Powers;
using _Scripts.Tiles;
using _Scripts.Tiles.Types;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Utility
{
    [CreateAssetMenu(menuName = "Installer/Prefab Installer")]
    public class PrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private TileView _tile;
        [FormerlySerializedAs("_powerSelectionView")] [SerializeField] private PowerView _powerView;
        [SerializeField] private MountainView _mountain;
        
        public override void InstallBindings()
        {
            BindViewFactory(_tile);
            BindViewFactory(_powerView);
            BindViewFactory(_mountain);
        }

        private void BindViewFactory<T>(T prefab) where T : Component
        {
            Container.Bind<IViewFactory<T>>()
                .FromMethod(context => context.Container.Instantiate<ViewFactory<T>>(new object[] {prefab}))
                .AsSingle();
        }
    }
}