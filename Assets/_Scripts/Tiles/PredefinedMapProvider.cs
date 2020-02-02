using System.Collections.Generic;
using _Scripts.Factions;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public class PredefinedMapProvider : IGridProvider
    {
        [Inject] private IGridEdit _gridEdit;
        [Inject] private ISpreadData _data;

        public IReadOnlyDictionary<Vector2Int, EnvironmentType> Tiles => _gridEdit.Tiles;
        public IEnumerable<StartPoint> StartPoints => _data.StartPoints;
    }
}