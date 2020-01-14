using DG.Tweening;
using UnityEngine;

namespace UI.Camera
{

    public class RoomStaticCamera : MonoBehaviour
    {
        [SerializeField] private float _approachRate, _power;
        [SerializeField] private UnityEngine.Camera _camera;
        public RectTransform BoundsRect;
        private Vector2 _lastMinOffset, _lastMaxOffset, _cameraPosition;
        private Vector2 _nextPos;
        [SerializeField] private int _inertiaPower;

        private void Start() => _cameraPosition = _nextPos;

        public void Update()
        {
            var objPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            objPos.z = 0;

            var camVertExtent = _camera.orthographicSize;
            var camHorzExtent = _camera.aspect * camVertExtent;

            var leftBound   = BoundsRect.offsetMin.x + camHorzExtent;
            var rightBound  = BoundsRect.offsetMax.x - camHorzExtent;
            var bottomBound = BoundsRect.offsetMin.y + camVertExtent;
            var topBound    = BoundsRect.offsetMax.y - camVertExtent;

            var camX = Mathf.Clamp(objPos.x, leftBound, rightBound);
            var camY = Mathf.Clamp(objPos.y, bottomBound, topBound);

            if (Input.GetMouseButton(1))
                _camera.transform.DOMove(new Vector3(camX, camY, -10), 1f);

            _nextPos.y = camX > 0 ? camY + _inertiaPower : camY - _inertiaPower;
        }
    }
}