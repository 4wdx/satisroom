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
        [SerializeField] private int _sortingOrderDelta = 10;
        
        private DragableObject _dragableObject;
        private SpriteRenderer _spriteRenderer;
        private int _defaultSortingLayer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _defaultSortingLayer = _spriteRenderer.sortingOrder;
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
            _spriteRenderer.sortingOrder = _defaultSortingLayer + _sortingOrderDelta;
            
            if (_startHandle) 
                _spriteRenderer.sprite = _onStartDragSprite;
        }

        private void OnEndHandle()
        {
            _spriteRenderer.sortingOrder = _defaultSortingLayer;
            
            if (_endHandle) 
                _spriteRenderer.sprite = _onEndDragSprite;
        }

        private void OnDisableHandle()
        {
            _spriteRenderer.sortingOrder = _defaultSortingLayer - _sortingOrderDelta;
            
            if (_disableHandle) 
                _spriteRenderer.sprite = _onDisableSprite;
        }
    }
}
