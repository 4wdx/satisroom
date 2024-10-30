using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public class HeartLevelEmotion : MonoBehaviour
    {
        [SerializeField] private Sprite _correctSprite;
        [SerializeField] private Sprite _uncorrectSprite;
        [SerializeField] private AudioClip _correctSound;
        [SerializeField] private AudioClip _uncorrectSound;
        [SerializeField] private float _showTime;

        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _spriteRenderer.enabled = false;
        }
        
        public void StartShow(bool result) => 
            StartCoroutine(ShowEmotion(result));

        private IEnumerator ShowEmotion(bool result)
        {
            print("Show");
            if (result == true)
            {
                _audioSource.clip = _correctSound;
                _audioSource.Play();
                _spriteRenderer.sprite = _correctSprite;
            }
            else
            {
                _audioSource.clip = _uncorrectSound;
                _audioSource.Play();
                _spriteRenderer.sprite = _uncorrectSprite;
            }
            
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(_showTime);
            _spriteRenderer.enabled = false;
        }
    }
}
