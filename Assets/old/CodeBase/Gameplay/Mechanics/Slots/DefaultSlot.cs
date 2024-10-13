using System;
using System.Collections;
using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class DefaultSlot : ItemSlot
    {
        public override event Action OnCompleted;

        [SerializeField] private DragableObject _linkedItem;
        
        public override void ContainsItem(DragableObject dragableObject) => 
            StartCoroutine(ValidateAfterFrame(dragableObject));

        private IEnumerator ValidateAfterFrame(DragableObject dragableObject)
        {
            yield return null;
         
            if (dragableObject == _linkedItem)
            {
                _linkedItem.Disable();
                _linkedItem.transform.parent = transform;
                _linkedItem.transform.localPosition = Vector2.zero;
                
                OnCompleted?.Invoke();
            }
        }
    }
}
