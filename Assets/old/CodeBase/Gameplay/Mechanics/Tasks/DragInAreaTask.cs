using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))] //for trigger work, dragable object don't has him 
    public class DragInAreaTask : Task
    {
        [SerializeField] private DragableObject _dragableObject;

        private void OnValidate() => 
            GetComponent<Rigidbody2D>().isKinematic = true;
        
        private void Start() => 
            GetComponent<Rigidbody2D>().isKinematic = true;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            if (!other.TryGetComponent(out DragableObject dragableObject)) return;

            if (dragableObject == _dragableObject)
            {
                print("enter");
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
                print("exit");
                _dragableObject.OnEndDrag -= StopTask;
                _dragableObject.OnEndDrag -= StopTask;
                OnComplete -= DisableDragableObject;
            }        
        }
        
        private void DisableDragableObject()
        {
            print("dfsgsdgf");
            _dragableObject.transform.position = transform.position;
            _dragableObject.Disable();
        }
    }
}
