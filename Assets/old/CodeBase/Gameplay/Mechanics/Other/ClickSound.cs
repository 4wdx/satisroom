using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(IClickable))]
    public class ClickSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _audioSource;
        private IClickable _clickable;
        
        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;

            _clickable = GetComponent<IClickable>();
            _clickable.OnClicked += PlaySound;
        }

        private void PlaySound()
        {
            _audioSource.clip = _clip;
            _audioSource.Play();
        }

        private void OnDestroy() => 
            _clickable.OnClicked -= PlaySound;
    }
}
