using System;
using _Scripts.Factions;
using _Scripts.Tiles;
using _Scripts.Utility;
using UniRx;
using Zenject;

namespace _Scripts.Powers
{
    public class ManaCreationSystem : Subscription
    {
        [Inject] private ITileModel _tile;
        [Inject] private IManaData _data;
        [Inject] private IManaModel _mana;

        private IDisposable _creation1;
        private IDisposable _creation2;
        private IDisposable _creation3;
        private IDisposable _creation4;
        private IDisposable _creation5;
        
        public override void Initialize()
        {
            var startDelay = _data.StartDelay.RandomValue();
            Observable.Timer(TimeSpan.FromSeconds(startDelay)).Subscribe(_ =>
            {
                _tile.Intensity(Faction.Nature).Subscribe(IntensityChanged).AddTo(_disposer);
            }).AddTo(_disposer);
        }

        private void IntensityChanged(int intensity)
        {
            for (var i = 0; i <= 5; i++)
            {
                var existingCreation = GetCreation(i);
                if (intensity < i && existingCreation != null) // Tree killed
                {
                    existingCreation.Dispose();
                    SetCreation(i, null);
                }
                else if (intensity >= i && existingCreation == null) // Tree planted 
                {
                    var creation = StartGenerationProcess(i);
                    SetCreation(i, creation);
                }
            }
        }

        private IDisposable StartGenerationProcess(int i)
        {
            var time = _data.TimeTilManaGrant.RandomValue(); 
            return Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(_ =>
                {
                    _mana.Mana.Value += _data.ManaGrantedPerTree;
                    _mana.ManaIncreasedBy.OnNext((_tile, i));
                    SetCreation(i, StartGenerationProcess(i));
                });
        }

        private IDisposable GetCreation(int i)
        {
            switch (i)
            {
                case 1: return _creation1;
                case 2: return _creation2;
                case 3: return _creation3;
                case 4: return _creation4;
                case 5: return _creation5;
                default: return null;
            }
        }

        private void SetCreation(int i, IDisposable creation)
        {
            switch (i)
            {
                case 1: _creation1 = creation; break;
                case 2: _creation2 = creation; break;
                case 3: _creation3 = creation; break;
                case 4: _creation4 = creation; break;
                case 5: _creation5 = creation; break;
            }
        }
    }
}