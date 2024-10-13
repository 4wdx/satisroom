using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(DragableObject))]
    public class ItemPutBehaviour : MonoBehaviour, IItemModificator
    {
        [SerializeField] private LayerMask _boxLayer;
        private DragableObject _dragableObject;

        public void Initialize(DragableObject dragableObject)
        {
            _dragableObject = dragableObject;
            _dragableObject.OnEndDrag += CheckBox;
        }

        public void Dispose()
        {
            _dragableObject.OnEndDrag -= CheckBox;
            _dragableObject = null;
        }

        private void CheckBox()
        {
            var colliders = Physics2D.OverlapPointAll(transform.position, _boxLayer);

            foreach (var collider in colliders)
            {
                if (collider == null) continue;

                if (collider.TryGetComponent(out ItemSlot slot)) 
                    slot.ContainsItem(_dragableObject);
            }
        }
    }
}
