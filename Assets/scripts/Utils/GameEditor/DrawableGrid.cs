using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.GameEditor
{
    public class DrawableGrid : MonoBehaviour
    {
        private CanvasRenderer _canvasRenderer;

        public int Height, Width;
        private static readonly int BaseColour = Shader.PropertyToID("_BaseColour");
        public Material Material;
        private BoxCollider2D _collider;

        public void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _canvasRenderer = GetComponent<CanvasRenderer>();
            _canvasRenderer.SetMaterial(Material, Texture2D.whiteTexture);
            _collider.size = new Vector2(Width * 64, Height * 64);
        }

        public void Initialize() //Initialize grid
        {
            var image = GetComponent<Image>();
            var rectTransform = image.rectTransform;
            var rect = rectTransform.sizeDelta;
            rect.x = Height * 64; //We need to set this properties as parts of tiled grids (scale 0.64)
            rect.y = Width * 64;
            rectTransform.sizeDelta = rect;
            Debug.Log($"{Height} {Width} {rect.x} {rect.y}");
        }

        public void UpdateState(Color color)
        {
            try
            {
                if (_canvasRenderer == null)
                    return;
                var material = _canvasRenderer.GetMaterial();
                material.SetColor(BaseColour, color);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}