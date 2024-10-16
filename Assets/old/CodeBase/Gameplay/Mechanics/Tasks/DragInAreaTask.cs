using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class DragInAreaTask : Task
    {
        [SerializeField] private DragableObject _dragableObject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            if (!other.TryGetComponent(out DragableObject dragableObject)) return;

            if (dragableObject == _dragableObject)
            {
                _dragableObject.OnEndDrag += StopTask;
                OnComplete += DisableDragableObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            if (!other.TryGetComponent(out DragableObject dragableObject)) return;

            if (dragableObject == _dragableObject)
            {
                _dragableObject.OnEndDrag -= StopTask;
                OnComplete -= DisableDragableObject;
            }        
        }
        
        private void DisableDragableObject()
        {
            _dragableObject.Disable();
            _dragableObject.enabled = false;
        }
    }
}
