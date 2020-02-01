using System.Collections.Generic;
using _Scripts.Factions;
using _Scripts.Growth;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public abstract class TileView : View
    {
        [SerializeField] private List<GameObject> _humans;
        [SerializeField] private List<GameObject> _nature;

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
        }
    }
}