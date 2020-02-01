using System.Collections.Generic;
using _Scripts.Factions;
using _Scripts.Growth;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public class TileView : View
    {
        [SerializeField] private GameObject _seaTile;
        [SerializeField] private GameObject _grasslandTile;
        [SerializeField] private GameObject _swampTile;
        [SerializeField] private GameObject _desertTile;
        [SerializeField] private GameObject _mountainTile;

        [SerializeField] private List<GameObject> _humans;
        [SerializeField] private List<GameObject> _nature;

        [Inject] private ITileModel _model;
        public ITileModel Model => _model;
        
        protected override void Install()
        {
            Container.Bind<ITileModel>().FromInstance(_model);
            
            _seaTile.SetActive(_model.Type.Value == EnvironmentType.Sea);
            _grasslandTile.SetActive(_model.Type.Value == EnvironmentType.Grassland);
            _swampTile.SetActive(_model.Type.Value == EnvironmentType.Swamp);
            _desertTile.SetActive(_model.Type.Value == EnvironmentType.Desert);
            _mountainTile.SetActive(_model.Type.Value == EnvironmentType.Mountain);
            
            Container.BindInterfacesTo<IntensityPresenter>().AsTransient().WithArguments(_humans, Faction.Humans);
            Container.BindInterfacesTo<IntensityPresenter>().AsTransient().WithArguments(_nature, Faction.Nature);
            
            Container.BindInterfacesTo<GrowthSystem>().AsTransient().WithArguments(Faction.Humans);
            Container.BindInterfacesTo<GrowthSystem>().AsTransient().WithArguments(Faction.Nature);
        }
    }
}