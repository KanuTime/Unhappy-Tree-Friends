using System.Collections.Generic;
using _Scripts.Factions;
using _Scripts.Growth;
using _Scripts.Powers;
using _Scripts.Utility;
using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public abstract class TileView : View
    {
        [SerializeField] private List<GameObject> _humans;
        [SerializeField] private List<GameObject> _nature;
        [SerializeField] private List<ParticleSystem> _manaGeneration;

        [Inject] private ITileModel _model;
        public ITileModel Model => _model;
        
        protected override void Install()
        {
            Container.Bind<ITileModel>().FromInstance(_model);
            
            Container.BindInterfacesTo<IntensityPresenter>().AsTransient().WithArguments(_humans, Faction.Humans);
            Container.BindInterfacesTo<IntensityPresenter>().AsTransient().WithArguments(_nature, Faction.Nature);
            
            Container.BindInterfacesTo<GrowthSystem>().AsTransient().WithArguments(Faction.Humans);
            Container.BindInterfacesTo<GrowthSystem>().AsTransient().WithArguments(Faction.Nature);
            
            Container.BindInterfacesTo<Spread>().AsSingle();
            
            Container.BindInterfacesTo<SpreadSystem>().AsTransient().WithArguments(Faction.Humans);
            Container.BindInterfacesTo<SpreadSystem>().AsTransient().WithArguments(Faction.Nature);
            
            Container.BindInterfacesTo<SpreadIncreaseSystem>().AsSingle();
            
            Container.BindInterfacesTo<DestroyOnEnvironmentChangeSystem>().AsSingle();
            Container.BindInterfacesTo<TileSoundSystem>().AsSingle();
            
            Container.BindInterfacesTo<ManaCreationSystem>().AsSingle();
            Container.BindInterfacesTo<ManaCreationPresenter>().AsSingle().WithArguments(_manaGeneration);
            
            Container.BindInterfacesTo<TornadoSystem>().AsSingle();
            Container.BindInterfacesTo<FloodSystem>().AsSingle();
            Container.BindInterfacesTo<FireSystem>().AsSingle();
            Container.BindInterfacesTo<AnimalAttackSystem>().AsSingle();
            Container.BindInterfacesTo<PollenSystem>().AsSingle();
        }
    }
}