using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public abstract class Task : MonoBehaviour
    {
        public event Action OnComplete;
        public event Action OnStart;

        protected bool IsActive { get; private set; }
        private ITaskModificator[] _taskModificators;
        
        private void Awake()
        {
            _taskModificators = GetComponents<ITaskModificator>();
            
            foreach (var modificator in _taskModificators) 
                modificator.Initialize(this);
        }

        private void OnDestroy()
        {
            foreach (var modificator in _taskModificators) 
                modificator.Dispose();
        }

        public virtual void StartTask()
        {
            IsActive = true;
            OnStart?.Invoke();
        }

        protected void StopTask()
        {
            DragHandler.Instance.CancelDrag();
            
            print("stop task");
            IsActive = false;
            OnComplete?.Invoke();
        }
    }
}
