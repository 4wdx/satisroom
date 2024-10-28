using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class SetActiveTaskModificator : MonoBehaviour, ITaskModificator
    {
        [SerializeField] private ActionType _actionType;
        [Space]
        [SerializeField] private GameObject[] _enabledGameObject;
        [SerializeField] private GameObject[] _disabledObject;
        
        private Task _task;
        
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
            
            foreach (var go in _enabledGameObject) 
                go.SetActive(true);

            foreach (var go in _disabledObject) 
                go.SetActive(false);
        }

        private void DoStopBindedAction()
        {
            if (_actionType != ActionType.OnStop) return;
            
            foreach (var go in _enabledGameObject) 
                go.SetActive(true);

            foreach (var go in _disabledObject) 
                go.SetActive(false);
        }
    }
}
