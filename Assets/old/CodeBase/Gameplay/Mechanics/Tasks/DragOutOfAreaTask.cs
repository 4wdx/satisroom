using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))] //for trigger work, dragable object don't has him 
    public class DragOutOfAreaTask : Task
    {
        [SerializeField] private DragableObject _dragableObject;
        
        private void OnValidate() => 
            GetComponent<Rigidbody2D>().isKinematic = true;

        private void Start()
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            _dragableObject.OnEndDrag += ReturnToStartPosition;
            OnComplete += DisableDragableObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            if (!other.TryGetComponent(out DragableObject dragableObject)) return;

            if (dragableObject == _dragableObject)
            {
                _dragableObject.OnEndDrag -= ReturnToStartPosition;
                _dragableObject.OnEndDrag += StopTask;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            if (!other.TryGetComponent(out DragableObject dragableObject)) return;

            if (dragableObject == _dragableObject)
            {
                _dragableObject.OnEndDrag += ReturnToStartPosition;
                _dragableObject.OnEndDrag -= StopTask;
            }
        }
        private void DisableDragableObject() => 
            _dragableObject.OnEndDrag -= StopTask;

        private void ReturnToStartPosition() => 
            _dragableObject.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                _dragableObject.transform.position.z); 
    }
}
