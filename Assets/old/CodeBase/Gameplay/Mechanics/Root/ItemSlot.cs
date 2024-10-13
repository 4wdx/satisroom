using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics.Root
{
    public abstract class ItemSlot : MonoBehaviour
    {
        public abstract event Action OnCompleted;

        public abstract void ContainsItem(DragableObject dragableObject);
    }
}
