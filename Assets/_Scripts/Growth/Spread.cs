using _Scripts.Factions;
using UniRx;

namespace _Scripts.Growth
{
    public interface ISpread
    {
        ISubject<Faction> Triggered { get; }
    }
    
    public class Spread : ISpread
    {
        public ISubject<Faction> Triggered { get; } = new Subject<Faction>();
    }
}