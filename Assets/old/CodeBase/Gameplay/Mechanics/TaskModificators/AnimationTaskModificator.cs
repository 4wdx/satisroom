using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class AnimationTaskModificator : MonoBehaviour, ITaskModificator
    {

        [SerializeField] private ActionType _actionType;
        [SerializeField] private Animator[] _animators;

        private const string START_KEY = "start";
        private const string STOP_KEY = "start";
        private Task _task;
        
        public void Initialize(Task task)
        {
            _task = task;
            _task.OnStart+= DoStartBindedAction;
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
            
            foreach (var animator in _animators)
            {
                animator.gameObject.SetActive(true);
                animator.SetTrigger(START_KEY);
            }
        }

        private void DoStopBindedAction()
        {
            if (_actionType != ActionType.OnStop) return;
            
            foreach (var animator in _animators)
            {
                animator.gameObject.SetActive(true);
                animator.SetTrigger(STOP_KEY);
            }
        }
    }
}
