using _Scripts.Tiles;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public interface IMousePositionController
    {
        IObservable<TileEditView> Clicked { get; }
        IObservable<TileEditView> Hovered { get; }
    }
    
    public class MousePositionController : ITickable, IMousePositionController
    {
        [Inject] private Camera _camera;
        [Inject] private LayerMask _tileLayer;

        public IObservable<TileEditView> Clicked => _clicked;
        private readonly Subject<TileEditView> _clicked = new Subject<TileEditView>();
        
        public IObservable<TileEditView> Hovered => _hovered;
        private readonly Subject<TileEditView> _hovered = new Subject<TileEditView>();
        
        public void Tick()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, _tileLayer))
            {
                var tile = hit.collider.gameObject.GetComponent<TileEditView>();
                _hovered.OnNext(tile);
                
                if (Input.GetMouseButtonDown(0))
                    _clicked.OnNext(tile);
            }
        }
    }
}