using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class PaintTask : Task
    {
        private enum PaintType {
            Paint,
            Erase
        }
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _brush;
        [SerializeField] private int _brushSize;
        [SerializeField] private PaintType _paintType;
        
        private Texture2D _texture;
        private Color[] _originalColors;
        
        private void Start()
        {
            _texture = _spriteRenderer.sprite.texture;
            _originalColors = _texture.GetPixels();

            if (_paintType == PaintType.Paint) 
                ClearTexture();
        }

        void Update()
        {
            if (IsActive == false) return;
            
            if (Input.GetKey(KeyCode.Mouse0)) 
                PaintPixel(GetTexturePosition(_brush.position));

            if (Input.GetKeyUp(KeyCode.Mouse0)) 
                CheckCompleted();
        }

        private void OnDisable() => 
            ResetTexture();
        

        private Vector2Int GetTexturePosition(Vector2 worldPosition)
        {
            Vector3 localPos = _spriteRenderer.transform.InverseTransformPoint(worldPosition);
            int x = Mathf.RoundToInt((localPos.x + _spriteRenderer.sprite.bounds.extents.x) * _texture.width / _spriteRenderer.sprite.bounds.size.x);
            int y = Mathf.RoundToInt((localPos.y + _spriteRenderer.sprite.bounds.extents.y) * _texture.height / _spriteRenderer.sprite.bounds.size.y);
            
            return new Vector2Int(x, y);
        }

        private void PaintPixel(Vector2Int pos)
        {
            for (int y = -_brushSize; y <= _brushSize; y++)
            {
                for (int x = -_brushSize; x <= _brushSize; x++)
                {
                    Vector2Int pixelCoord = new Vector2Int(pos.x + x, pos.y + y);

                    if (pixelCoord.x < 0 || pixelCoord.x >= _texture.width || pixelCoord.y < 0 || pixelCoord.y >= _texture.height) continue;
                    if (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) > Mathf.Pow(_brushSize - 0.5f, 2)) continue;
                    
                    if (_paintType == PaintType.Erase)
                        _texture.SetPixel(pixelCoord.x, pixelCoord.y, Color.clear);
                    else 
                        _texture.SetPixel(pixelCoord.x, pixelCoord.y, 
                            _originalColors[pixelCoord.x + pixelCoord.y * _texture.width]);
                    
                }
            }
            
            _texture.Apply();
        }

        private void CheckCompleted()
        {
            var pixels = _texture.GetPixels();
            var paintedPixels = 0;

            switch (_paintType)
            {
                case PaintType.Erase:
                {
                    foreach (Color pixel in pixels)
                        if (pixel.a < 0.1) paintedPixels++;

                    break;
                }
                case PaintType.Paint:
                {
                    foreach (var pixel in pixels)
                        if (pixel.a > 0.9) paintedPixels++;
                    
                    break;
                }
            }

            if (paintedPixels > _originalColors.Length * 0.95)
            {
                StopTask();
                switch (_paintType)
                {
                    case PaintType.Erase:
                        ClearTexture();
                        break;
                    case PaintType.Paint:
                        ResetTexture();
                        break;
                }
            }
        }
        
        private void ResetTexture()
        {
            if (_originalColors == null) return; 
            
            _texture.SetPixels(_originalColors);
            _texture.Apply();
        }

        private void ClearTexture()
        {
            for (int y = 0; y <= _texture.height; y++)
            {
                for (int x = 0; x <= _texture.width; x++)
                {
                    _texture.SetPixel(x, y, Color.clear);
                }
            }
            _texture.Apply();
        }
    }
}
