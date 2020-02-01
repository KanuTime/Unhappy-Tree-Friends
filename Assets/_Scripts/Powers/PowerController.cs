﻿using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class PowerController : Subscription
    {
        [Inject] private IMousePositionController mousePositionController;

        private PowerType selectedPower;

        public override void Initialize()
        {
            mousePositionController.Clicked.Subscribe(OnTileClicked).AddTo(_disposer);
        }

        private void OnTileClicked(TileView tileView)
        {
            Debug.Log($"Tile {tileView.Position.x},{tileView.Position.y} clicked");

        }
    }
}