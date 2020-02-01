using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class CameraDragMovementSystem : ITickable
    {
        [Inject] private Camera _camera;
        [Inject] private LayerMask _layer;
        [Inject] private float _speed;

        private Vector3 _start = Vector3.zero;
        private bool _dragging;
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _start = Input.mousePosition;
                _dragging = true;
                return;
            }

            if (Input.GetMouseButtonUp(1))
                _dragging = false;

            if (_dragging)
            {
                var direction = (_start - Input.mousePosition).normalized;
                var translatedRotation = _camera.transform.rotation * new Vector3(direction.x, 0, direction.y);
                translatedRotation.y = 0;

                _camera.transform.position += translatedRotation * Time.deltaTime * _speed;
                _start = Input.mousePosition;
            }
        }
    }
}