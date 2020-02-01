using _Scripts.Powers;
using _Scripts.Tiles.Types;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Utility
{
    [CreateAssetMenu(menuName = "Installer/Prefab Installer")]
    public class PrefabInstaller : ScriptableObjectInstaller
    {
        [Header("Tiles")]
        [SerializeField] private MountainView _mountain;
        [SerializeField] private GrasslandView _grassland;
        [SerializeField] private DesertView _desert;
        [SerializeField] private SwampView _swamp;
        [SerializeField] private SeaView _sea;

        [Header("Effects")]
        [SerializeField] private TornadoView _tornado;
        [SerializeField] private FloodView _flood;
        
        public override void InstallBindings()
        {
            BindViewFactory(_mountain);
            BindViewFactory(_grassland);
            BindViewFactory(_desert);
            BindViewFactory(_swamp);
            BindViewFactory(_sea);

            BindViewFactory(_tornado);
            BindViewFactory(_flood);
        }

        private void BindViewFactory<T>(T prefab) where T : Component
        {
            Container.Bind<IViewFactory<T>>()
                .FromMethod(context => context.Container.Instantiate<ViewFactory<T>>(new object[] {prefab}))
                .AsSingle();
        }
    }
}