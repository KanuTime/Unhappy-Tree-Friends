using _Scripts.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Scripts
{
    public interface IMousePositionController
    {
        IObservable<ITileModel> Clicked { get; }
        IObservable<ITileModel> Hovered { get; }
    }
    
    public class MousePositionController : ITickable, IMousePositionController
    {
        [Inject] private Camera _camera;
        [Inject] private LayerMask _tileLayer;

        public IObservable<ITileModel> Clicked => _clicked;
        private readonly Subject<ITileModel> _clicked = new Subject<ITileModel>();
        
        public IObservable<ITileModel> Hovered => _hovered;
        private readonly Subject<ITileModel> _hovered = new Subject<ITileModel>();
        
        public void Tick()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, _tileLayer))
            {
                var tile = hit.collider.gameObject.GetComponent<TileView>();
                if (tile == null)
                    return;
                
                _hovered.OnNext(tile.Model);
                
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    _clicked.OnNext(tile.Model);
            }
        }
    }
}