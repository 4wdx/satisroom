using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class BlockingDoor : MonoBehaviour, IClickable
    {
        public event Action OnClicked;
        public bool Opened { get; private set; }
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Click()
        {
            Opened = !Opened;
            _animator.SetBool("opened", Opened);
            OnClicked?.Invoke();
        }
    }
}
