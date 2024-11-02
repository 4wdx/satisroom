using CodeBase.Gameplay.Mechanics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Utils
{
    public class RaycastBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData) => 
            DragHandler.Instance.Disable();
        
        public void OnPointerExit(PointerEventData eventData) => 
            DragHandler.Instance.Enable();
    }
}
