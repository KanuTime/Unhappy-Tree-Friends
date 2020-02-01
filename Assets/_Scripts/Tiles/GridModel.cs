using UniRx;

namespace _Scripts.Tiles
{
    public interface IGridModel
    {
        IObservable<ITileModel> ClickedOnTile { get; }
    }
    
    public class GridModel : IGridModel
    {
        private readonly ISubject<ITileModel> _clickedOnTile = new Subject<ITileModel>();
        public IObservable<ITileModel> ClickedOnTile => _clickedOnTile;
    }
}