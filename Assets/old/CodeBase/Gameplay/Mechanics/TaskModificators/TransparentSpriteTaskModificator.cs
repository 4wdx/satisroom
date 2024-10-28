using DG.Tweening; 
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class TransparentSpriteTaskModificator : MonoBehaviour, ITaskModificator
    {
        [SerializeField] private ActionType _actionType;
        [Space]
        [SerializeField] private SpriteRenderer[] _enabledSprite;
        [SerializeField] private SpriteRenderer[] _disabledSprite;
        [SerializeField] private float _transitionDuration;
        
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
            
            foreach (var sprite in _disabledSprite)
                sprite.DOFade(0f, _transitionDuration).onComplete 
                    += () => sprite.gameObject.SetActive(false);

            foreach (var sprite in _enabledSprite)
            {
                sprite.gameObject.SetActive(true);
                sprite.DOFade(1f, _transitionDuration);
            }
        }
        
        private void DoStopBindedAction()
        {
            if (_actionType != ActionType.OnStop) return;
            
            foreach (var sprite in _disabledSprite)
                sprite.DOFade(0f, _transitionDuration).onComplete 
                    += () => sprite.gameObject.SetActive(false);

            foreach (var sprite in _enabledSprite)
            {
                sprite.gameObject.SetActive(true);
                sprite.DOFade(1f, _transitionDuration);
            }
        }
    }
}
