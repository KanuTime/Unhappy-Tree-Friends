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
        
        [Header("Buttons")]
        [SerializeField] private Button _powerWindButton;
        [SerializeField] private Image _powerWindImage;
        [SerializeField] private Image _powerWindCooldownImage;
        [SerializeField] private Button _powerEarthButton;
        [SerializeField] private Image _powerEarthImage;
        [SerializeField] private Image _powerEarthCooldownImage;
        [SerializeField] private Button _powerWaterButton;
        [SerializeField] private Image _powerWaterImage;
        [SerializeField] private Image _powerWaterCooldownImage;

        [Header("Config")] 
        [SerializeField] private float _environmentChangeDelay;
        
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
            Container.BindInterfacesTo<PowerPresenter>().AsCached()
                .WithArguments(_powerWindButton, PowerType.Wind, _powerWindImage, _powerWindCooldownImage);
            Container.BindInterfacesTo<PowerPresenter>().AsCached()
                .WithArguments(_powerEarthButton, PowerType.Earth, _powerEarthImage, _powerEarthCooldownImage);
            Container.BindInterfacesTo<PowerPresenter>().AsCached()
                .WithArguments(_powerWaterButton, PowerType.Water, _powerWaterImage, _powerWaterCooldownImage);

            Container.BindInterfacesTo<PowerConsequenceController>().AsSingle();
            Container.BindInterfacesTo<EffectSystem>().AsSingle();
            
            Container.BindInterfacesTo<KillingEffectSystem>().AsSingle();
            Container.BindInterfacesTo<EnvironmentChangeEffectSystem>().AsSingle().WithArguments(_environmentChangeDelay);
            
            Container.BindInterfacesTo<BalanceModel>().AsSingle();
            Container.BindInterfacesTo<BalancePresenter>().AsSingle().WithArguments(_natureIntensity, _humanIntensity);
        }
    }
}