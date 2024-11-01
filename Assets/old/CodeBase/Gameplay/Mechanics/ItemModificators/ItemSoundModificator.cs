using CodeBase.Cfg;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class ItemSoundModificator : MonoBehaviour, IItemModificator
    {
        [SerializeField] private AudioClip _onStartDragSound;
        [SerializeField] private AudioClip _dragSound;
        [SerializeField] private AudioClip _onEndDragSound;
        [SerializeField] private AudioClip _onDisableSound;

        private DragableObject _dragableObject;
        private AudioSource _audioSource;
        private AudioSource _loopedAudioSource;

        private void Awake()
        {
            SoundPack soundPack = Resources.Load<SoundPack>("DefaultSound");
            
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = soundPack.SoundGroup;
            _audioSource.playOnAwake = false;    

            _loopedAudioSource = gameObject.AddComponent<AudioSource>();
            _loopedAudioSource.playOnAwake = false;
            _loopedAudioSource.loop = true;
            _loopedAudioSource.outputAudioMixerGroup = soundPack.SoundGroup;
            
            if (_onStartDragSound == null ) 
                _onStartDragSound = soundPack.StartDragSound;
            
            if (_onEndDragSound == null)
                _onEndDragSound = soundPack.EndDragSound;
            
            if (_onDisableSound == null)
                _onDisableSound = soundPack.DisableDragSound;
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

            if (_dragSound)
            {
                _loopedAudioSource.clip = _dragSound;
                _loopedAudioSource.Play();
            }
        }

        private void OnEndHandle()
        {
            _audioSource.clip = _onEndDragSound;
            _audioSource.Play();
            
            if (_dragSound)
            {
                _loopedAudioSource.clip = _dragSound;
                _loopedAudioSource.Stop();
            }
        }
        
        private void OnDisableHandle() 
        {
            _audioSource.clip = _onDisableSound;
            _audioSource.Play();
            
            if (_dragSound)
            {
                _loopedAudioSource.clip = _dragSound;
                _loopedAudioSource.Stop();
            }
        }
    }
}
