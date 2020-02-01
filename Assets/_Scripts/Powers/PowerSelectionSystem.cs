using System;
using _Scripts.Utility;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerSelectionSystem : IInitializable
    {
        [Inject] private Canvas _canvas;
        [Inject] private IViewFactory<PowerView> _viewFactory;

        public void Initialize()
        {
            var posX = 30;
            var posY = 30;

            foreach (var powerType in Enum.GetValues(typeof(PowerType)))
            {
                _viewFactory.Create(new Vector3(posX, posY, 0), powerType);
                posX += 80;
            }
        }
    }
}