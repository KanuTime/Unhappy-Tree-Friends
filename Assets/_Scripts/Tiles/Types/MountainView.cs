using DefaultNamespace;
using UnityEngine;

namespace _Scripts.Tiles.Types
{
    public class MountainView : TileView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _duration;
        
        protected override void Install()
        {
            base.Install();
            
            Container.BindInterfacesTo<EarthquakeSystem>().AsSingle().WithArguments(_animator, _duration);
        }
    }
}