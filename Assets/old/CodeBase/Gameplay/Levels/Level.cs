using System;
using CodeBase.Gameplay.Mechanics;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public abstract class Level : MonoBehaviour
    {
        public abstract event Action<bool> OnCompleted;

        private void Awake()
        {
            DragHandler.Instance.Enable();
            OnCompleted += StopDragHandler;
        }
        
        private void StopDragHandler(bool obj) => 
            DragHandler.Instance.Disable();
    }
}