using UnityEngine;
using Zenject;

namespace _Scripts.Utility
{
    [CreateAssetMenu(menuName = "Installer/Prefab Installer")]
    public class PrefabInstaller : ScriptableObjectInstaller
    {
        
        public override void InstallBindings()
        {
        }

        private void BindViewFactory<T>(T prefab) where T : Component
        {
            Container.Bind<IViewFactory<T>>()
                .FromMethod(context => context.Container.Instantiate<ViewFactory<T>>(new object[] {prefab}))
                .AsSingle();
        }
    }
}