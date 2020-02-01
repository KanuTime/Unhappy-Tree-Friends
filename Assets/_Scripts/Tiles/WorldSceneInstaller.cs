using _Scripts.Balance;
using _Scripts.Effects;
using _Scripts.Powers;
using _Scripts.Tiles;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Slider _natureIntensity;
        [SerializeField] private Slider _humanIntensity;
        [SerializeField] private Button _powerWindButton;
        [SerializeField] private Image _powerWindImage;
        [SerializeField] private Button _powerEarthButton;
        [SerializeField] private Image _powerEarthImage;
        [SerializeField] private Button _powerWaterButton;
        [SerializeField] private Image _powerWaterImage;
        
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
            Container.BindInterfacesTo<PowerController>().AsCached().WithArguments(_powerWindButton, PowerType.Wind);
            Container.BindInterfacesTo<PowerController>().AsCached().WithArguments(_powerEarthButton, PowerType.Earth);
            Container.BindInterfacesTo<PowerController>().AsCached().WithArguments(_powerWaterButton, PowerType.Water);
            Container.BindInterfacesTo<PowerPresenter>().AsCached().WithArguments(_powerWindButton, PowerType.Wind, _powerWindImage);
            Container.BindInterfacesTo<PowerPresenter>().AsCached().WithArguments(_powerEarthButton, PowerType.Earth, _powerEarthImage);
            Container.BindInterfacesTo<PowerPresenter>().AsCached().WithArguments(_powerWaterButton, PowerType.Water, _powerWaterImage);

            Container.BindInterfacesTo<PowerConsequenceController>().AsSingle();
            Container.BindInterfacesTo<EffectSystem>().AsSingle();
            
            Container.BindInterfacesTo<KillingEffectSystem>().AsSingle();
            Container.BindInterfacesTo<EnvironmentChangeEffectSystem>().AsSingle();
            
            Container.BindInterfacesTo<BalanceModel>().AsSingle();
            Container.BindInterfacesTo<BalancePresenter>().AsSingle().WithArguments(_natureIntensity, _humanIntensity);
        }
    }
}