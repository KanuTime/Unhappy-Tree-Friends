using System.Collections.Generic;
using _Scripts.Factions;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public class IntensityPresenter : Subscription
    {
        [Inject] private List<GameObject> _gameObjects;
        [Inject] private ITileModel _model;
        [Inject] private Faction _faction;
        
        public override void Initialize()
        {
            _model.Intensity(_faction).Subscribe(DisplayIntensity).AddTo(_disposer);
        }

        private void DisplayIntensity(int degree)
        {
            for (var i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].SetActive(degree > i);
        }
    }
}