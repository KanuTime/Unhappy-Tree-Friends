using System.Collections.Generic;
using System.Linq;
using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Balance
{
    public interface IBalanceModel
    {
        IReactiveProperty<int> NatureIntensity { get; }
        IReactiveProperty<int> HumansIntensity { get; }
        int MaxAmount { get; }
    }
    
    public class BalanceModel : Subscription, IBalanceModel
    {
        [Inject] private IGridModel _grid;
        
        private readonly ReactiveProperty<int> _natureIntensity = new ReactiveProperty<int>();
        public IReactiveProperty<int> NatureIntensity => _natureIntensity;
        
        private readonly ReactiveProperty<int> _humansIntensity = new ReactiveProperty<int>();
        public IReactiveProperty<int> HumansIntensity => _humansIntensity;
        
        public int MaxAmount { get; private set; }

        public override void Initialize()
        {
            var tiles = _grid.Tiles.ToList();
            MaxAmount = tiles.Count * 5;

            CountProgress(tiles);

            foreach (var tile in tiles)
            {
                tile.Intensity(Faction.Humans).SkipLatestValueOnSubscribe().Subscribe(_ => CountProgress(tiles)).AddTo(_disposer);
                tile.Intensity(Faction.Nature).SkipLatestValueOnSubscribe().Subscribe(_ => CountProgress(tiles)).AddTo(_disposer);
            }
        }

        private void CountProgress(IEnumerable<ITileModel> tiles)
        {
            var natureProgress = 0;
            var humansProgress = 0;
            
            foreach (var tile in tiles)
            {
                humansProgress += tile.Intensity(Faction.Humans).Value;
                natureProgress += tile.Intensity(Faction.Nature).Value;
            }

            _natureIntensity.Value = natureProgress;
            _humansIntensity.Value = humansProgress;
        }
    }
}