using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Gameplay.Mechanics
{
    public class ItemChildSpritesModificator: MonoBehaviour, IItemModificator
    {
        [SerializeField] private int _orderDelta = 10;
        
        private DragableObject _dragableObject;
        private SpriteRenderer[] _spriteRenderers;
        private int[] _baseOrders;

        private void Awake()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _baseOrders = new int[_spriteRenderers.Length];
            
            for (int i = 0; i < _spriteRenderers.Length; i++)
                _baseOrders[i] = _spriteRenderers[i].sortingOrder;
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
            for (int i = 0; i < _spriteRenderers.Length; i++)
                _spriteRenderers[i].sortingOrder = _baseOrders[i] + _orderDelta;
        }

        private void OnEndHandle()
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
                _spriteRenderers[i].sortingOrder = _baseOrders[i];
        }

        private void OnDisableHandle()
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
                _spriteRenderers[i].sortingOrder = _baseOrders[i] - _orderDelta;
        }
    }
}
