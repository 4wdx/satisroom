using System;
using CodeBase.Gameplay.Mechanics;
using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public class HeartLevel : Level
    {
        public override event Action<bool> OnCompleted;
        public event Action<int> OnHealthChanged;

        [SerializeField] private Task[] _tasks;
        [SerializeField] private int _health;
        private int _currentTaskIndex;
        
        private void Start()
        {
            _tasks[0].OnComplete += StartNextTask;
            _tasks[0].StartTask();
            
            DragHandler.Instance.OnDamagedClick += DecreaseHealth;
        }
        
        private void DecreaseHealth()
        {
            _health--;
            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                _tasks[_currentTaskIndex].OnComplete -= StartNextTask;
                DragHandler.Instance.OnDamagedClick -= DecreaseHealth;
                OnCompleted?.Invoke(false);
            }
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
