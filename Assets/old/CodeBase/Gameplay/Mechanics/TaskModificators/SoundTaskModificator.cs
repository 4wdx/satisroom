using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class SoundTaskModificator : MonoBehaviour, ITaskModificator
    {

        [SerializeField] private ActionType _actionType;
        [Space]
        [SerializeField] private AudioClip _audioClip;
        
        private AudioSource _audioSource;
        private Task _task;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(Task task)
        {
            _task = task;
            _task.OnStart += DoStartBindedAction;
            _task.OnComplete += DoStopBindedAction;
        }
        
        public void Dispose()
        {
            _task.OnStart -= DoStartBindedAction;
            _task.OnComplete -= DoStopBindedAction;
            
            _task = null;
        }
        
        private void DoStartBindedAction()
        {
            if (_actionType != ActionType.OnStart) return;

            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
        
        private void DoStopBindedAction()
        {
            if (_actionType != ActionType.OnStop) return;

            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
}
