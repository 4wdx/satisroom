using System;
using System.Collections;
using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics.Slots
{
    public class BoxSlot : ItemSlot
    {
        public override event Action OnCompleted;
        public event Action<bool> OnStateChanged; 
        
        [SerializeField] private DragableObject[] _linkedObjects;
        
        private int _completedCount;
        private bool _completed;
        private bool _isOpened;

        private void Update()
        {
            if (_completed) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                
                if (collider != null && collider.gameObject == gameObject)
                {
                    _isOpened = !_isOpened;
                    OnStateChanged?.Invoke(_isOpened);
                }
            }
        }

        public override void ContainsItem(DragableObject dragableObject) => 
            StartCoroutine(ValidateAfterFrame(dragableObject));

        private IEnumerator ValidateAfterFrame(DragableObject dragableObject)
        {
            yield return null;
            
            if (_isOpened == false) yield break;
            
            foreach (var linkedObject in _linkedObjects)
            {
                if (dragableObject == linkedObject)
                {
                    dragableObject.Disable();
                    dragableObject.transform.parent = transform;
                    dragableObject.transform.localPosition = Vector2.zero;
                    
                    CheckCompleted();
                }
            }
        }

        private void CheckCompleted()
        {
            _completedCount++;

            if (_completedCount == _linkedObjects.Length)
            {
                _completed = true;
                OnStateChanged?.Invoke(false);
                OnCompleted?.Invoke();
            }
        }
    }
}
