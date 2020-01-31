using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Tiles
{
    public class EnvironmentView : MonoBehaviour
    {
        [Serializable]
        private class Entry
        {
            public EnvironmentType Type;
            public Material Material;
        }
        
        [SerializeField] private EnvironmentType _type;
        [SerializeField] private Renderer _sprite;
        [SerializeField] private List<Entry> _colors;

        private void OnValidate()
        {
            _sprite.material = _colors.First(entry => entry.Type == _type).Material;
        }
    }
}