using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class CameraKeyMovementSystem : IFixedTickable
    {
        [Inject] private Camera _camera;
        [Inject] private float _speed;
        
        public void FixedTick()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            var translatedRotation = _camera.transform.rotation * new Vector3(horizontal, 0, vertical);
            translatedRotation.y = 0;
            
            _camera.transform.position += translatedRotation; 
        }
    }
}