using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(DragableObject))]
    public class ItemSpriteModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private Sprite _onStartDragSprite;
        [SerializeField] private Sprite _onEndDragSprite;
        [SerializeField] private Sprite _onDisableSprite;

        [SerializeField] private bool _startHandle;
        [SerializeField] private bool _endHandle;
        [SerializeField] private bool _disableHandle;
        
        private DragableObject _dragableObject;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;
            
            _dragableObject.OnStartDrag += OnStartHandle;
            _dragableObject.OnEndDrag += OnEndHandle;
            _dragableObject.OnDisable += OnDisableHandle;
        }
        
        public void Dispose()
        {
            _dragableObject.OnStartDrag -= OnStartHandle;
            _dragableObject.OnEndDrag -= OnEndHandle;
            _dragableObject.OnDisable -= OnDisableHandle;

            _dragableObject = null;
        }

        private void OnStartHandle()
        {
            _spriteRenderer.sortingOrder = 10;
            
            if (_startHandle) 
                _spriteRenderer.sprite = _onStartDragSprite;
        }

        private void OnEndHandle()
        {
            _spriteRenderer.sortingOrder = 0;
            
            if (_endHandle) 
                _spriteRenderer.sprite = _onEndDragSprite;
        }

        private void OnDisableHandle()
        {
            _spriteRenderer.sortingOrder = -10;
            
            if (_disableHandle) 
                _spriteRenderer.sprite = _onDisableSprite;
        }
    }
}
