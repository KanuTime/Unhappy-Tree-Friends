using System.Collections.Generic;
using _Scripts.Factions;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Tiles
{
    public class NatureDegreePresenter : Subscription
    {
        [Inject] private List<GameObject> _gameObjects;
        [Inject] private ITileModel _model;
        
        public override void Initialize()
        {
            _model.Nature.Subscribe(DisplayNature).AddTo(_disposer);
        }

        private void DisplayNature(int degree)
        {
            for (var i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].SetActive(degree > i);
        }
    }
}