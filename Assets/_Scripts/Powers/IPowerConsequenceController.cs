using _Scripts.Effects;
using _Scripts.Tiles;
using UniRx;

namespace _Scripts.Powers
{
	public interface IPowerConsequenceController
	{
		IObservable<(ConsequenceType, ITileModel)> ConsequenceTileTrigger { get; }
	}
}