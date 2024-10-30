using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class PaintTask : Task
    {
        private enum PaintType
        {
            Paint,
            Erase
        }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _brush;
        [SerializeField] private int _brushSize;
        [SerializeField] private PaintType _paintType;

        private Texture2D _texture;
        private Sprite _newSprite;
        private Color[] _originalColors;
        private int _clearPixelsCount;

        private void OnEnable()
        {
            Sprite tempSprite = _spriteRenderer.sprite;

            _texture = new Texture2D(tempSprite.texture.width, tempSprite.texture.height);
            _texture.SetPixels(tempSprite.texture.GetPixels());
            _texture.Apply();

            _originalColors = _texture.GetPixels();
            print(_texture.GetPixels().Length);
            
            foreach (Color pixel in _originalColors)
            {
                if (pixel.a < 0.001)
                    _clearPixelsCount++;
            }
            
            print(_clearPixelsCount);

            Vector2 pivot = new Vector2(tempSprite.pivot.x / _texture.width, tempSprite.pivot.y / _texture.height);
            _newSprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), pivot, tempSprite.pixelsPerUnit);
            _spriteRenderer.sprite = _newSprite;
            
            if (_paintType == PaintType.Paint)
                ClearTexture();
        }

        private void Update()
        {
            if (IsActive == false) return;

            Vector3 brushPos = _brush.position;
            brushPos.z = _spriteRenderer.transform.position.z;
            if (Input.GetKey(KeyCode.Mouse0))
                PaintPixel(GetTexturePosition(brushPos));

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
                    {
                        if (pixel.a < 0.001)
                            paintedPixels++;
                    }
                    break;
                }
                case PaintType.Paint:
                {
                    foreach (var pixel in pixels)
                    {
                        if (pixel.a > 0.001)
                            paintedPixels++;
                    }
                    paintedPixels += _clearPixelsCount;
                    break;
                }
            }

            print(paintedPixels + " / " + _originalColors.Length);
            if (paintedPixels  > _originalColors.Length * 0.95)
            {
                switch (_paintType)
                {
                    case PaintType.Erase:
                        ClearTexture();
                        break;
                    case PaintType.Paint:
                        ResetTexture();
                        break;
                }
                
                StopTask();
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