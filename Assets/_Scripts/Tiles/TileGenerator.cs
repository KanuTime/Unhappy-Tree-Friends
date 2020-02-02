using System.Collections.Generic;
using System.Linq;
using _Scripts.Factions;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace _Scripts.Tiles
{
    public class TileGenerator : IGridProvider
    {
        [Inject] private int _width;
        [Inject] private int _height;
        [Inject] private int _humanSpawnPoints;
        [Inject] private int _natureSpawnPoints;

        public IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles
        {
            get
            {
                var result = new Dictionary<Vector2Int, EnvironmentType>();
                
                for (var x = 0; x < _width; x++)
                for (var y = 0; y < _height; y++)
                {
                    var env = (EnvironmentType) Random.Range(0, 5); 
                    result.Add(new Vector2Int(x, y), env);
                }
                
                return result;
            }
        }

        public IEnumerable<StartPoint> StartPoints
        {
            get
            {
                var result = new Dictionary<Vector2Int, (StartPoint, StartPoint)>();
                AddSpawnPoints(result, Faction.Humans, _humanSpawnPoints);
                AddSpawnPoints(result, Faction.Nature, _natureSpawnPoints);
                var list = result.SelectMany(pair => new [] {pair.Value.Item1, pair.Value.Item2}).Where(x => x != null).ToList();
                Assert.AreEqual(_humanSpawnPoints, list.Where(point => point.Faction == Faction.Humans).Sum(point => point.Intensity));
                Assert.AreEqual(_natureSpawnPoints, list.Where(point => point.Faction == Faction.Nature).Sum(point => point.Intensity));
                return list;
            }
        }

        private void AddSpawnPoints(Dictionary<Vector2Int, (StartPoint, StartPoint)> result, Faction faction, int spawnPoints)
        {
            for (var i = 0; i < spawnPoints; i++)
            {
                var position = new Vector2Int(Random.Range(0, _width), Random.Range(0, _height));
                var (humans, nature) = result.ContainsKey(position) ? result[position] : (null, null);

                var startPoint = faction == Faction.Humans ? humans : nature;
                if (startPoint != null)
                {
                    startPoint.Intensity++;
                }
                else
                {
                    var newPoint = new StartPoint
                    {
                        Faction = faction,
                        Intensity = 1,
                        Position = position
                    };
                    result[position] = faction == Faction.Humans ? (newPoint, nature) : (humans, newPoint);
                }
            }
        }
    }
}