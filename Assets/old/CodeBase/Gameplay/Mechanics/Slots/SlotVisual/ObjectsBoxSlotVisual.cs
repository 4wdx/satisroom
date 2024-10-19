using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    [RequireComponent(typeof(BoxSlot))]
    public class ObjectsBoxSlotVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _openedVisual;
        [SerializeField] private GameObject _closedVisual;
        private BoxSlot _boxSlot;

        private void Awake()
        {
            if (_boxSlot == null)
                _boxSlot = GetComponent<BoxSlot>();
        }

        private void OnEnable() => 
            _boxSlot.OnStateChanged += SetAnimation;

        private void OnDisable() => 
            _boxSlot.OnStateChanged -= SetAnimation;

        private void SetAnimation(bool isOpened)
        {
            _openedVisual.SetActive(isOpened);
            _closedVisual.SetActive(!isOpened);
        }
    }
}
