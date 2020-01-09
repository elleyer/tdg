using UnityEngine;

namespace ScriptsData.Utils.GameEditor
{
    public class GridProvider : MonoBehaviour
    {
        private DrawableGrid _drawableGrid;
        [SerializeField] private GameObject _emptyImage;
        [SerializeField] private Transform _parent;
        [SerializeField] private Material _material;

        public void CreateGrid(int h, int w)
        {
            _drawableGrid = Instantiate(_emptyImage, _parent).GetComponent<DrawableGrid>();
            _drawableGrid.Material = _material;
            _drawableGrid.Height = h;
            _drawableGrid.Width = w;
            _drawableGrid.Initialize();
        }

        public void UpdateGridProperties(Vector3 newPos, Color color, bool placeAble)
        {
            _drawableGrid.transform.position = new Vector3(newPos.x, newPos.y, 0);
            _drawableGrid.UpdateState(color);
        }

        public void RemoveGrid() => Destroy(_drawableGrid.gameObject);

        public bool IsAvailable => _drawableGrid != null;
    }
}