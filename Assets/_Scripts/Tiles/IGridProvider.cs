using System.Collections.Generic;
using _Scripts.Factions;
using UnityEngine;

namespace _Scripts.Tiles
{
    public interface IGridProvider
    {
        IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles { get; }
        IEnumerable<StartPoint> StartPoints { get; }
    }
}