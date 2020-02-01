using _Scripts.Tiles;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class WorldSceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _tileLayer;
        [SerializeField] private GridView _grid;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera);
            Container.BindInterfacesTo<MousePositionController>().AsSingle().WithArguments(_tileLayer);
            Container.BindInterfacesTo<MousePositionLogger>().AsSingle();
        }
    }
}