using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Factions;
using _Scripts.Tiles.Types;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public interface IGridModel
    {
        IEnumerable<ITileModel> AdjacentTiles(Vector2Int position);
        IEnumerable<ITileModel> Tiles { get; }
    }
    
    public class GridModel : Subscription, IGridModel
    {
        private const int K_offset = 5;
        
        [Inject] private IGridProvider _gridData;
        
        [Inject] private IViewFactory<MountainView> _mountainFactory;
        [Inject] private IViewFactory<GrasslandView> _grassFactory;
        [Inject] private IViewFactory<DesertView> _desertFactory;
        [Inject] private IViewFactory<SwampView> _swampFactory;
        [Inject] private IViewFactory<SeaView> _seaFactory;

        private readonly Dictionary<Vector2Int, ITileModel> _tiles = new Dictionary<Vector2Int, ITileModel>();
        private int _maxX;
        private int _maxY;
        
        public override void Initialize()
        {
            var tiles = _gridData.Tiles;
            foreach (var tile in tiles)
            {
                var startPoints = _gridData.StartPoints.Where(point => point.Position == tile.Key).ToList();
                var humanityDegree = startPoints.Where(point => point.Faction == Faction.Humans).Sum(point => point.Intensity); 
                var natureDegree = startPoints.Where(point => point.Faction == Faction.Nature).Sum(point => point.Intensity); 
                
                var tileModel = new TileModel(tile.Key, tile.Value, humanityDegree, natureDegree);
                _tiles[tile.Key] = tileModel;

                var position = new Vector3(tile.Key.x - K_offset, 0, tile.Key.y - K_offset);
                tileModel.Type.Subscribe(type => CreateView(position, tileModel, type)).AddTo(_disposer);

                if (tile.Key.x > _maxX)
                    _maxX = tile.Key.x;
                if (tile.Key.y > _maxY)
                    _maxY = tile.Key.y;
            }
        }

        private void CreateView(Vector3 position, ITileModel model, EnvironmentType type)
        {
            switch (type)
            {
                case EnvironmentType.Sea:
                    _seaFactory.Create(position, model);
                    break;
                case EnvironmentType.Grassland:
                    _grassFactory.Create(position, model);
                    break;
                case EnvironmentType.Swamp:
                    _swampFactory.Create(position, model);
                    break;
                case EnvironmentType.Desert:
                    _desertFactory.Create(position, model);
                    break;
                case EnvironmentType.Mountain:
                    _mountainFactory.Create(position, model);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<ITileModel> AdjacentTiles(Vector2Int position)
        {
            var adjacentTiles = new List<ITileModel>();

            if (position.x > 0)
                adjacentTiles.Add(_tiles[new Vector2Int(position.x - 1, position.y)]);
            
            if (position.y > 0)
                adjacentTiles.Add(_tiles[new Vector2Int(position.x, position.y - 1)]);
            
            if (position.x < _maxX)
                adjacentTiles.Add(_tiles[new Vector2Int(position.x + 1, position.y)]);
            
            if (position.y < _maxY)
                adjacentTiles.Add(_tiles[new Vector2Int(position.x, position.y + 1)]);

            return adjacentTiles;
        }

        public IEnumerable<ITileModel> Tiles => _tiles.Values;
    }
}