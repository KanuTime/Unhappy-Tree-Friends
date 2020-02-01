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
        [FormerlySerializedAs("_powerSelectionView")] [SerializeField] private PowerView _powerView;
        [SerializeField] private MountainView _mountain;
        [SerializeField] private GrasslandView _grassland;
        [SerializeField] private DesertView _desert;
        [SerializeField] private SwampView _swamp;
        [SerializeField] private SeaView _sea;
        
        public override void InstallBindings()
        {
            BindViewFactory(_powerView);
            BindViewFactory(_mountain);
            BindViewFactory(_grassland);
            BindViewFactory(_desert);
            BindViewFactory(_swamp);
            BindViewFactory(_sea);
        }

        private void BindViewFactory<T>(T prefab) where T : Component
        {
            Container.Bind<IViewFactory<T>>()
                .FromMethod(context => context.Container.Instantiate<ViewFactory<T>>(new object[] {prefab}))
                .AsSingle();
        }
    }
}