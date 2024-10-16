using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class CollectTask : Task
    {
        [SerializeField] private List<CollectingItem> _collectingItems;
        [SerializeField] private float _moveTime = 0.5f;
        private int _collectedCount; 
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsActive == false) return;
            
            foreach (var item in _collectingItems.ToList())
            {
                if (other.TryGetComponent(out CollectingItem i) != item) continue;
                
                i.Collect(transform, _moveTime);
                _collectingItems.Remove(i);
                
                if (_collectingItems.Count == 0)
                    StopTask();
            }
        }
    }
}
