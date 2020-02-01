using System;
using System.Collections.Generic;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerSelectionSystem : IInitializable
    {
        [Inject] private Canvas _canvas;
        [Inject] private IViewFactory<PowerView> _viewFactory;

        private const int ButtonSize = 200;
        private static readonly List<PowerType> PowerTypes = new List<PowerType>
        {
            PowerType.Wind,
            PowerType.Earth,
            PowerType.Water
        };

        public void Initialize()
        {
            var posX = ButtonSize / 2;
            const int posY = ButtonSize / 2;

            foreach (var powerType in PowerTypes)
            {
                _viewFactory.Create(new Vector3(posX, posY, 0), powerType);
                posX += ButtonSize + 20;
            }
        }
    }
}