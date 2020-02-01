using _Scripts.Powers;
using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class WorldSceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private LayerMask _tileLayer;
        [SerializeField] private GridEditView gridEdit;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera);
            Container.BindInstance(_canvas);
            Container.Bind<IGridEdit>().FromInstance(gridEdit);
            
            Container.BindInterfacesTo<GridModel>().AsSingle();
            
            Container.BindInterfacesTo<MousePositionController>().AsSingle().WithArguments(_tileLayer);
            Container.BindInterfacesTo<MousePositionLogger>().AsSingle();

            Container.BindInterfacesTo<PowerModel>().AsSingle();
            Container.BindInterfacesTo<PowerSelectionSystem>().AsSingle().WithArguments(_canvas);
        }
    }
}