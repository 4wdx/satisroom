using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics.Root
{
    public class DragableObject : MonoBehaviour
    {
        public event Action OnStartDrag;
        public event Action OnDrag;
        public event Action OnEndDrag;
        public event Action OnDisable;

        private IItemModificator[] _itemModificators;

        private void Start()
        {
            _itemModificators = GetComponents<IItemModificator>();
            
            foreach (var itemModificator in _itemModificators) 
                itemModificator.Initialize(this);
        }

        private void OnDestroy()
        {
            foreach (var itemModificator in _itemModificators) 
                itemModificator.Dispose();
        }

        public void StartDrag() => 
            OnStartDrag?.Invoke();

        public void Drag() => 
            OnDrag?.Invoke();

        public void EndDrag() => 
            OnEndDrag?.Invoke();

        public void Disable() => 
            OnDisable?.Invoke();
    }
}