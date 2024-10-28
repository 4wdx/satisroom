using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class PositionReturnerModificator : MonoBehaviour, IItemModificator
    {
        private DragableObject _dragableObject;
        private Vector3 _startPosition;

        private void Awake() => 
            _startPosition = transform.position;

        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;
            
            _dragableObject.OnEndDrag += OnEndHandle;
            _dragableObject.OnDisable += OnDisableHandle;
        }
        public void Dispose()
        {
            _dragableObject.OnEndDrag -= OnEndHandle;
            _dragableObject.OnDisable -= OnDisableHandle;

            _dragableObject = null;
        }

        private void OnEndHandle() => 
            transform.position = _startPosition;

        private void OnDisableHandle() => 
            transform.position = _startPosition;
    }
}
