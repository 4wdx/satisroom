using CodeBase.Gameplay.Mechanics.Root;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(DragableObject))]
    public class ItemTransformModificator : MonoBehaviour, IItemModificator
    {
        [Header("Base Values")]
        [SerializeField] private Vector3 _baseRotation;
        [SerializeField] private Vector3 _baseScale;
        [SerializeField] private bool _autoSet;
        
        [Header("On Drag Values")]
        [SerializeField] private Vector3 _targetRotation;
        [SerializeField] private Vector3 _targetScale;
        
        [Header("On Complete Values")]
        [SerializeField] private Vector3 _disableRotation;
        [SerializeField] private Vector3 _disableScale;
        
        [Space]
        [SerializeField] private float _transitDuration;
        [SerializeField] private bool _disableCollider;
        
        private DragableObject _dragableObject;
        private Collider2D _collider;
        
        private void Awake()
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            
            _baseRotation = transform.rotation.eulerAngles;
            _baseScale = transform.localScale;
        }

        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;
            
            _dragableObject.OnStartDrag += OnStartDragHandle;
            _dragableObject.OnEndDrag += OnEndDragHandle;
            _dragableObject.OnDisable += OnDisableHandle;
        }

        public void Dispose()
        {
            _dragableObject.OnStartDrag -= OnStartDragHandle;
            _dragableObject.OnEndDrag -= OnEndDragHandle;
            _dragableObject.OnDisable -= OnDisableHandle;

            _dragableObject = null;
        }

        private void OnStartDragHandle()
        {
            transform.DOLocalRotate(_targetRotation, _transitDuration);
            transform.DOScale(_targetScale, _transitDuration);
        }
        
        private void OnEndDragHandle()
        {
            transform.DOLocalRotate(_baseRotation, _transitDuration);
            transform.DOScale(_baseScale, _transitDuration);
        }

        private void OnDisableHandle()
        {
            DOTween.Kill(transform);
            
            if (_disableCollider)
                _collider.enabled = false;
            
            transform.DOLocalRotate(_disableRotation, _transitDuration);
            transform.DOScale(_disableScale, _transitDuration);
        }
    }
}
