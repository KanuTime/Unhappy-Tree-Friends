using _Scripts.Utility;
using UnityEngine;

namespace DefaultNamespace
{
    public class FireView : View
    {
        [SerializeField] private float _duration;
        
        protected override void Install()
        {
            Container.BindInterfacesTo<TimedDestructionSystem>().AsSingle().WithArguments(_duration);
        }
    }
}