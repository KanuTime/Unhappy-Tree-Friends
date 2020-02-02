using _Scripts.Tiles;
using UniRx;
using Zenject;

namespace _Scripts.Powers
{
    public interface IManaModel
    {
        IReactiveProperty<float> Mana { get; }  
        ISubject<(ITileModel, int)> ManaIncreasedBy { get; } 
    }
    
    public class ManaModel : IManaModel
    {
        public IReactiveProperty<float> Mana { get; }
        public ISubject<(ITileModel, int)> ManaIncreasedBy { get; } = new Subject<(ITileModel, int)>();

        public ManaModel(IManaData data)
        {
            Mana = new ReactiveProperty<float>(data.StartValue);
        }
    }
}