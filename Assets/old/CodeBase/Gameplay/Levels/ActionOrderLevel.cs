using System;
using CodeBase.Gameplay.Mechanics;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public class ActionOrderLevel : Level
    {
        public override event Action<bool> OnCompleted;

        [SerializeField] private Task[] _tasks;
        private int _currentTaskIndex;
        
        private void Start()
        {
            _tasks[0].OnComplete += StartNextTask;
            _tasks[0].StartTask();
        }

        private void StartNextTask()
        {
            _tasks[_currentTaskIndex].OnComplete -= StartNextTask;
            _currentTaskIndex++;

            if (_currentTaskIndex < _tasks.Length)
            {
                _tasks[_currentTaskIndex].OnComplete += StartNextTask;
                _tasks[_currentTaskIndex].StartTask();
            }
            else
                OnCompleted?.Invoke(true);
        }
    }
}
