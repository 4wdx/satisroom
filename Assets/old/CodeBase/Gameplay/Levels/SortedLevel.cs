using System;
using CodeBase.Gameplay.Mechanics;
using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public class SortedLevel : Level
    {
        public override event Action<bool> OnCompleted;
        
        [SerializeField] private ItemSlot[] _slots;
        private int _completedCount;

        private void OnEnable() {
            foreach (ItemSlot slot in _slots) slot.OnCompleted += CheckCompleted;
        }

        private void OnDisable() {
            foreach (ItemSlot slot in _slots) slot.OnCompleted -= CheckCompleted;
        }
        
        private void CheckCompleted()
        {
            _completedCount++;

            if (_completedCount == _slots.Length)
            {
                OnCompleted?.Invoke(true);
                print("completed");
            }
        }
    }
}
