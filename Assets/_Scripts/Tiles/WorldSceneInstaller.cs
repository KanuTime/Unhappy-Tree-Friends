using _Scripts.Effects;
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
        [SerializeField] private float _cameraMovementSpeed;
        [SerializeField] private LayerMask _panningLayer;
        [SerializeField] private SoundEffects _soundEffects;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera);
            Container.BindInstance(_canvas);
            
            Container.BindInterfacesTo<CameraKeyMovementSystem>().AsSingle().WithArguments(_cameraMovementSpeed);
            Container.BindInterfacesTo<CameraDragMovementSystem>().AsSingle().WithArguments(_panningLayer, _cameraMovementSpeed * 3);
            
            Container.Bind<IGridEdit>().FromInstance(gridEdit);
            Container.BindInterfacesTo<SoundEffects>().FromInstance(_soundEffects);
            
            Container.BindInterfacesTo<GridModel>().AsSingle();
            
            Container.BindInterfacesTo<MousePositionController>().AsSingle().WithArguments(_tileLayer);
            Container.BindInterfacesTo<MousePositionLogger>().AsSingle();

            Container.BindInterfacesTo<SelectedPowerModel>().AsSingle();
            Container.BindInterfacesTo<PowerSelectionSystem>().AsSingle().WithArguments(_canvas);

            Container.BindInterfacesTo<PowerConsequenceController>().AsSingle();
            Container.BindInterfacesTo<EffectSystem>().AsSingle();
            
            Container.BindInterfacesTo<KillingEffectSystem>().AsSingle();
            
        }
    }
}