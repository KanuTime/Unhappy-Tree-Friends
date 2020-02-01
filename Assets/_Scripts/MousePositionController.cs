using _Scripts.Tiles;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts
{
    public interface IMousePositionController
    {
        IObservable<TileView> Clicked { get; }
        IObservable<TileView> Hovered { get; }
    }
    
    public class MousePositionController : ITickable, IMousePositionController
    {
        [Inject] private Camera _camera;
        [Inject] private LayerMask _tileLayer;

        public IObservable<TileView> Clicked => _clicked;
        private readonly Subject<TileView> _clicked = new Subject<TileView>();
        
        public IObservable<TileView> Hovered => _hovered;
        private readonly Subject<TileView> _hovered = new Subject<TileView>();
        
        public void Tick()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, _tileLayer))
            {
                var tile = hit.collider.gameObject.GetComponent<TileView>();
                _hovered.OnNext(tile);
                
                if (Input.GetMouseButtonDown(0))
                    _clicked.OnNext(tile);
            }
        }
    }
}