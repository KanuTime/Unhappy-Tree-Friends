using UnityEngine;

namespace _Scripts.Tiles
{
    public class TileEditView : MonoBehaviour
    {
        [SerializeField] private EnvironmentType _type;

        [Space]
        [SerializeField] private GameObject _seaTile;
        [SerializeField] private GameObject _grasslandTile;
        [SerializeField] private GameObject _swampTile;
        [SerializeField] private GameObject _desertTile;
        [SerializeField] private GameObject _mountainTile;

        private void OnValidate()
        {
            _seaTile.SetActive(_type == EnvironmentType.Sea);
            _grasslandTile.SetActive(_type == EnvironmentType.Grassland);
            _swampTile.SetActive(_type == EnvironmentType.Swamp);
            _desertTile.SetActive(_type == EnvironmentType.Desert);
            _mountainTile.SetActive(_type == EnvironmentType.Mountain);
        }

        public EnvironmentType Type => _type;
    }
}