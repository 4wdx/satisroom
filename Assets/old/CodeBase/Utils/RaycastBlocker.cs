using System;
using CodeBase.Gameplay.Mechanics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Utils
{
    public class RaycastBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        private bool _isWorking;
        
        private void Update() => 
            _isWorking = !Input.GetKey(KeyCode.Mouse0);

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isWorking == false) return;
            
            DragHandler.Instance.Disable();
            DragHandler.Instance.CancelDrag();
        }

        public void OnPointerExit(PointerEventData eventData) => 
            DragHandler.Instance.Enable();
    }
}
