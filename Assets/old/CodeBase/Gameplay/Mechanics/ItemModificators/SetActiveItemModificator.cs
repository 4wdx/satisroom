using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class SetActiveItemModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private GameObject[] _gameObjects;
        
        [Space]
        [SerializeField] private bool _dragHandle;
        [SerializeField] private bool _stateWhileDraging;
        [Space]
        [SerializeField] private bool _disableHandle;
        [SerializeField] private bool _stateAfterDisable;
        
        private DragableObject _dragableObject;
        
        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;

            _dragableObject.OnStartDrag += OnStartDragHandle;
            _dragableObject.OnEndDrag += OnEndDragHandle;
            _dragableObject.OnDisable += DisableDragHandle;
        }
        public void Dispose()
        {
            _dragableObject.OnStartDrag -= OnStartDragHandle;
            _dragableObject.OnEndDrag -= OnEndDragHandle;
            _dragableObject.OnDisable -= DisableDragHandle;
            
            _dragableObject = null;
        }

        private void OnStartDragHandle()
        {
            if (_dragHandle == false) return;

            foreach (var go in _gameObjects) 
                go.SetActive(_stateWhileDraging);
        }

        private void OnEndDragHandle()
        {
            if (_dragHandle == false) return;

            foreach (var go in _gameObjects) 
                go.SetActive(!_stateWhileDraging);
        }

        private void DisableDragHandle()
        {
            if (_disableHandle == false) return;

            foreach (var go in _gameObjects) 
                go.SetActive(_stateAfterDisable);
        }
    }
}
