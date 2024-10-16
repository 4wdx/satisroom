using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Mechanics
{
    public class CollectingItem : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Collect(Transform collectorTransform, float moveTime)
        {
            transform.parent = collectorTransform;
            transform.DOLocalMove(Vector2.zero, moveTime);

            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        }
    }
}
