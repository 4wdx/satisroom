using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class AnimatorItemModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _dragKeyName;
        [SerializeField] private string _stopAnimKey;
        private DragableObject _dragableObject;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
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
        
        private void OnStartHandle() => 
            _animator.SetBool(_dragKeyName, true);

        private void OnEndHandle() => 
            _animator.SetBool(_dragKeyName, false);

        private void OnDisableHandle()
        {
            if (_stopAnimKey == "") return;
            
            _animator.SetTrigger(_stopAnimKey);
        }
    }
}
