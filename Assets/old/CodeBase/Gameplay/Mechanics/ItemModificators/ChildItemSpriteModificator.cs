using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class ChildItemSpriteModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private Sprite _onStartDragSprite;
        [SerializeField] private Sprite _onEndDragSprite;
        [SerializeField] private Sprite _onDisableSprite;

        [SerializeField] private bool _startHandle;
        [SerializeField] private bool _endHandle;
        [SerializeField] private bool _disableHandle;
        
        private DragableObject _dragableObject;
        private SpriteRenderer _spriteRenderer;
  
        [SerializeField] private SpriteRenderer _parentSpriteRenderer;
        private bool _attached;

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

        private void Update()
        {
            if (_attached)
                _spriteRenderer.sortingOrder = _parentSpriteRenderer.sortingOrder + 1;
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
            _spriteRenderer.sortingOrder = 11;
            
            if (_startHandle) 
                _spriteRenderer.sprite = _onStartDragSprite;
        }

        private void OnEndHandle()
        {
            _spriteRenderer.sortingOrder = 1;
            
            if (_endHandle) 
                _spriteRenderer.sprite = _onEndDragSprite;
        }

        private void OnDisableHandle()
        {
            _attached = true;
            
            if (_disableHandle) 
                _spriteRenderer.sprite = _onDisableSprite;
        }
    }
}
