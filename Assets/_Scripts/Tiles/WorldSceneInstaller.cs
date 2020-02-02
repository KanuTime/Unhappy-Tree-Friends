using _Scripts.Balance;
using _Scripts.Effects;
using _Scripts.Music;
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
        [SerializeField] private Text _manaText;
        
        [Header("Buttons")]
        [SerializeField] private Button _powerWindButton;
        [SerializeField] private Image _powerWindImage;
        [SerializeField] private Image _powerWindCooldownImage;
        [SerializeField] private Text _powerWindCost;
        
        [SerializeField] private Button _powerEarthButton;
        [SerializeField] private Image _powerEarthImage;
        [SerializeField] private Image _powerEarthCooldownImage;
        [SerializeField] private Text _powerEarthCost;
        
        [SerializeField] private Button _powerWaterButton;
        [SerializeField] private Image _powerWaterImage;
        [SerializeField] private Image _powerWaterCooldownImage;
        [SerializeField] private Text _powerWaterCost;
        
        [Header("Config")] 
        [SerializeField] private float _environmentChangeDelay;
        [SerializeField] private bool _generateWorldRandomly;
        [SerializeField] private Vector2Int _dimensions;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera);
            Container.BindInstance(_canvas);
            
            Container.BindInterfacesTo<CameraKeyMovementSystem>().AsSingle().WithArguments(_cameraMovementSpeed);
            Container.BindInterfacesTo<CameraDragMovementSystem>().AsSingle().WithArguments(_panningLayer, _cameraMovementSpeed * 3);

            if (_generateWorldRandomly)
                Container.BindInterfacesTo<TileGenerator>().AsSingle().WithArguments(_dimensions.x, _dimensions.y);
            else Container.Bind<IGridEdit>().FromInstance(gridEdit);
            
            Container.BindInterfacesTo<GridModel>().AsSingle();
            
            Container.BindInterfacesTo<SoundEffects>().FromInstance(_soundEffects);
            
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
            
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(_powerWindCost, PowerType.Wind);
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(_powerEarthCost, PowerType.Earth);
            Container.BindInterfacesTo<PowerCostPresenter>().AsCached().WithArguments(_powerWaterCost, PowerType.Water);
            
            Container.BindInterfacesTo<PowerConsequenceController>().AsSingle();
            Container.BindInterfacesTo<EffectSystem>().AsSingle();
            
            Container.BindInterfacesTo<KillingEffectSystem>().AsSingle();
            Container.BindInterfacesTo<SpawnEffectSystem>().AsSingle();
            Container.BindInterfacesTo<EnvironmentChangeEffectSystem>().AsSingle().WithArguments(_environmentChangeDelay);
            
            Container.BindInterfacesTo<BalanceModel>().AsSingle();
            Container.BindInterfacesTo<BalancePresenter>().AsSingle().WithArguments(_natureIntensity, _humanIntensity);

            Container.BindInterfacesTo<MusicController>().AsSingle().WithArguments(_natureIntensity, _humanIntensity);
            
            Container.BindInterfacesTo<ManaModel>().AsSingle();
            Container.BindInterfacesTo<ManaPresenter>().AsSingle().WithArguments(_manaText);
        }
    }
}