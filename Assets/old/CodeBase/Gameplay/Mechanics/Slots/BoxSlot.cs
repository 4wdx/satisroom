using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class BoxSlot : ItemSlot, IClickable
    {
        public event Action OnClicked;
        public override event Action OnCompleted;
        public event Action<bool> OnStateChanged; 
        
        [SerializeField] private DragableObject[] _linkedObjects;
        
        private int _completedCount;
        private bool _completed;
        private bool _isOpened;

        public void Click()
        {
            if (_completed) return;
            
            _isOpened = !_isOpened;
            OnStateChanged?.Invoke(_isOpened);
            OnClicked?.Invoke();
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
