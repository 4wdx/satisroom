using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(ItemSoundModificator))]
    public class DragableObject : MonoBehaviour
    {
        public event Action OnStartDrag;
        public event Action OnDrag;
        public event Action OnEndDrag;
        public event Action OnDisable;

        [Tooltip("GameObject for manage DragableObject state. If not null, activity of this component equals to object in field activity in hierarchy, else always true")]
        [SerializeField] private GameObject _stateProvider;
        public bool IsActive {
            get {
                if (_useStateProvider)
                    return _stateProvider.activeInHierarchy;
                
                return true;
            }
            
        }
        private IItemModificator[] _itemModificators;
        private bool _useStateProvider;

        private void Start()
        {
            _itemModificators = GetComponents<IItemModificator>();
            
            foreach (IItemModificator itemModificator in _itemModificators) 
                itemModificator.Initialize(this);

            _useStateProvider = _stateProvider != null;
        }

        private void OnDestroy()
        {
            if (_itemModificators == null) return;
            
            foreach (IItemModificator itemModificator in _itemModificators) 
                itemModificator.Dispose();
        }

        public void StartDrag()
        {
            if (IsActive == false) return;
            
            OnStartDrag?.Invoke();
        }

        public void Drag()
        {
            if (IsActive == false) return;

            OnDrag?.Invoke();
        }

        public void EndDrag()
        {
            if (IsActive == false) return;

            OnEndDrag?.Invoke();
        }

        public void Disable()
        {
            OnDisable?.Invoke();
        }
    }
}