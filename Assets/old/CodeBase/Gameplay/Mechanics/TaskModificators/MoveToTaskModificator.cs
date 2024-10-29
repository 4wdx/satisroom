using DG.Tweening;
using UnityEngine;


namespace CodeBase.Gameplay.Mechanics
{
    public class MoveToTaskModificator : MonoBehaviour, ITaskModificator
    {
        [SerializeField] private ActionType _actionType;
        [Space]
        [SerializeField] private Transform[] _movedTransforms;
        [SerializeField] private Transform _targetPosition;
        [SerializeField] private float _moveDuration;
        [SerializeField] private bool _activeAfterMove;
        
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
            
            foreach (var tr in _movedTransforms)
            {
                var targetVector = _targetPosition.position - tr.position;
                targetVector.z = tr.position.y;
                print("move");
                
                tr.DOMove(targetVector, _moveDuration).onComplete 
                    += () => tr.gameObject.SetActive(_activeAfterMove);
            }
        }

        private void DoStopBindedAction()
        {
            if (_actionType != ActionType.OnStop) return;
            
            foreach (var tr in _movedTransforms)
            {
                Vector3 movingVector = _targetPosition.position - tr.position;
                movingVector.z = 0;
                print("move");
                
                tr.DOMove(tr.position + movingVector, _moveDuration).onComplete 
                    += () => tr.gameObject.SetActive(_activeAfterMove);
            }
        }
    }
}
