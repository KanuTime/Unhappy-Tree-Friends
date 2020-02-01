using DefaultNamespace;
using UnityEngine;

namespace _Scripts.Tiles.Types
{
    public class DesertView : TileView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _duration;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Vector2 _timeBetweenDuneWinds;
        
        protected override void Install()
        {
            base.Install();
            
            Container.BindInterfacesTo<TornadoSystem>().AsSingle();
            Container.BindInterfacesTo<EarthquakeSystem>().AsSingle().WithArguments(_animator, _duration);
            Container.BindInterfacesTo<DuneAnimationSystem>().AsSingle().WithArguments(_particle, _timeBetweenDuneWinds);
        }
    }
}