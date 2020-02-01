using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Tiles
{
    public class DestroyOnEnvironmentChangeSystem : Subscription
    {
        [Inject] private ITileModel _model;
        [Inject] private ISelfDestruction _selfDestruction;
        
        public override void Initialize()
        {
            _model.Type.Pairwise().Where(pair => pair.Current != pair.Previous)
                .Subscribe(_selfDestruction.Destroy).AddTo(_disposer);
        }
    }
}