using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics.Slots
{
    [RequireComponent(typeof(DefaultSlot))]
    public class DefaultSlotVisual : MonoBehaviour
    {

        [SerializeField] private GameObject _defaultView;
        [SerializeField] private GameObject _completedView;
        private DefaultSlot _defaultSlot;

        private void Awake()
        {
            if (_defaultSlot == null)
                _defaultSlot = GetComponent<DefaultSlot>();
        }

        private void OnEnable() => 
            _defaultSlot.OnCompleted += ChangeView;

        private void OnDisable() => 
            _defaultSlot.OnCompleted -= ChangeView;

        private void ChangeView()
        {
            _defaultView.SetActive(false);
            _completedView.SetActive(true);
        }
    }
}
