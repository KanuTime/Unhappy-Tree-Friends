using System.Collections.Generic;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Powers
{
    public class ManaCreationPresenter : Subscription
    {
        [Inject] private List<ParticleSystem> _trees;
        [Inject] private IManaModel _model;
        [Inject] private ITileModel _tile;
        
        public override void Initialize()
        {
            _model.ManaIncreasedBy
                .Where(effect => effect.Item1 == _tile)
                .Subscribe(ShowEffect).AddTo(_disposer);
        }

        private void ShowEffect((ITileModel, int tree) increase)
        {
            var tree = _trees[increase.tree - 1];
            tree.gameObject.SetActive(true);
            tree.Play();
        }
    }
}