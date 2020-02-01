using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Factions;
using _Scripts.Tiles.Types;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public interface IGridModel
    {
        IEnumerable<ITileModel> AdjacentTiles(Vector2Int position);
    }
    
    public class GridModel : IGridModel, IInitializable
    {
        private const int K_offset = 5;
        
        [Inject] private IGridEdit _gridEdit;
        [Inject] private ISpreadData _spreadData;
        
        [Inject] private IViewFactory<TileView> _viewFactory;
        [Inject] private IViewFactory<MountainView> _mountainFactory;
        [Inject] private IViewFactory<GrasslandView> _grassFactory;
        [Inject] private IViewFactory<DesertView> _desertFactory;
        [Inject] private IViewFactory<SwampView> _swampFactory;

        private readonly Dictionary<Vector2Int, ITileModel> _tiles = new Dictionary<Vector2Int, ITileModel>();
        private int _maxX;
        private int _maxY;
        
        public void Initialize()
        {
            var tiles = _gridEdit.Tiles;
            foreach (var tile in tiles)
            {
                var startPoints = _spreadData.StartPoints.Where(point => point.Position == tile.Key).ToList();
                var humanityDegree = startPoints.Where(point => point.Faction == Faction.Humans).Sum(point => point.Intensity); 
                var natureDegree = startPoints.Where(point => point.Faction == Faction.Nature).Sum(point => point.Intensity); 
                
                var tileModel = new TileModel(tile.Key, tile.Value, humanityDegree, natureDegree);
                _tiles[tile.Key] = tileModel;
                CreateView(new Vector3(tile.Key.x - K_offset, 0, tile.Key.y - K_offset), tileModel);

                if (tile.Key.x > _maxX)
                    _maxX = tile.Key.x;
                if (tile.Key.y > _maxY)
                    _maxY = tile.Key.y;
            }
        }

        private void CreateView(Vector3 position, ITileModel model)
        {
            switch (model.Type.Value)
            {
                case EnvironmentType.Sea:
                    _viewFactory.Create(position, model);
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
    }
}