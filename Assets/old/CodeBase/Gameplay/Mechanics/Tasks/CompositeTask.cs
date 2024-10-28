using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class CompositeTask : Task
    {
        [SerializeField] private Task[] _tasks;

        private int _completedCount;
        
        public override void StartTask()
        {
            base.StartTask();

            foreach (var task in _tasks)
            {
                task.StartTask();
                task.OnComplete += IncreaseCompletedCount;
            }
        }
        
        private void IncreaseCompletedCount()
        {
            if (IsActive == false) return;
            
            _completedCount++;

            if (_completedCount == _tasks.Length)
            {
                foreach (var task in _tasks)
                {
                    task.OnComplete -= IncreaseCompletedCount;
                    StopTask();
                }
            }
        }
    }
}
