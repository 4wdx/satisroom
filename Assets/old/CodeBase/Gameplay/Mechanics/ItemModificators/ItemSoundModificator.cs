using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class ItemSoundModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private AudioClip _onStartDragSound;
        [SerializeField] private AudioClip _onEndDragSound;
        [SerializeField] private AudioClip _onDisableSound;

        private DragableObject _dragableObject;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;
            
            _dragableObject.OnStartDrag += OnStartHandle;
            _dragableObject.OnEndDrag += OnEndHandle;
            _dragableObject.OnDisable += OnDisableHandle;
        }
        
        public void Dispose()
        {
            _dragableObject.OnStartDrag -= OnStartHandle;
            _dragableObject.OnEndDrag -= OnEndHandle;
            _dragableObject.OnDisable -= OnDisableHandle;

            _dragableObject = null;
        }

        private void OnStartHandle()
        {
            _audioSource.clip = _onStartDragSound;
            _audioSource.Play();
        }

        private void OnEndHandle()
        {
            _audioSource.clip = _onEndDragSound;
            _audioSource.Play();
        }
        
        private void OnDisableHandle() 
        {
            _audioSource.clip = _onDisableSound;
            _audioSource.Play();
        }
    }
}
