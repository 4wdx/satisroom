using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class MultipleSlot : ItemSlot
    {
        private enum SetPositionType {
            InClosest,
            InOrderPair,
            InNextSlot
        }
        
        public override event Action OnCompleted;

        [SerializeField] private DragableObject[] _linkedObjects;
        [SerializeField] private List<Transform> _positions;
        [SerializeField] private SetPositionType _setPositionType;

        private int _completedCount;

        public override void ContainsItem(DragableObject dragableObject) => 
            StartCoroutine(ValidateAfterFrame(dragableObject));

        private IEnumerator ValidateAfterFrame(DragableObject dragableObject)
        {
            yield return null;
            for (int i = 0; i < _linkedObjects.Length; i++)
            {
                if (_linkedObjects[i] == dragableObject)
                {
                    dragableObject.Disable();
                    dragableObject.transform.parent = transform;
                    dragableObject.transform.position = GetPosition(i, dragableObject.transform);
                    
                    CheckCompleted();
                }
            }
        }

        private void CheckCompleted()
        {
            _completedCount++;
            
            if (_completedCount == _linkedObjects.Length)
                OnCompleted?.Invoke();
        }

        private Vector3 GetPosition(int indexInArray, Transform dragableObject)
        {
            Vector3 position;
            
            switch (_setPositionType)
            {
                case SetPositionType.InClosest:
                {
                    float minDistance = float.MaxValue;
                    Transform selectedPos = null;

                    foreach (var pos in _positions)
                    {
                        if (Vector2.Distance(pos.position, dragableObject.position) < minDistance)
                        {
                            minDistance = Vector2.Distance(pos.position, dragableObject.position);
                            selectedPos = pos;
                        }
                    }

                    position = selectedPos.position;
                    _positions.Remove(selectedPos);
                    break;
                }
                
                //----
                case SetPositionType.InOrderPair:
                    position = _positions[indexInArray].position;
                    break;
                
                //----
                case SetPositionType.InNextSlot:
                    position = _positions[0].position;
                    _positions.Remove(_positions[0]);
                    break;
                
                //----
                default:
                    throw new Exception("Validate type is uncorrect");
            }
            
            return position;
        }
    }
}
