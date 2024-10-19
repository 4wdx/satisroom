using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class DoorSlot : ItemSlot
    {
        public override event Action OnCompleted;

        [SerializeField] private DragableObject _linkedItem;
        [SerializeField] private BlockingDoor _blockingDoor;
        private bool _completed;
        
        public override void ContainsItem(DragableObject dragableObject)
        {
            if (_blockingDoor.Opened == false) return;

            StartCoroutine(ValidateAfterFrame(dragableObject));
        }
        
        private IEnumerator ValidateAfterFrame(DragableObject dragableObject)
        {
            yield return null;
         
            if (dragableObject == _linkedItem)
            {
                _linkedItem.Disable();
                _linkedItem.transform.parent = transform;
                _linkedItem.transform.localPosition = Vector2.zero;

                yield return new WaitUntil(() => _blockingDoor.Opened == false);
                print("completed door");
                OnCompleted?.Invoke();
            }
        }
    }
}
