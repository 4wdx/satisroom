using CodeBase.Utils;
using UnityEngine;
using YG;

namespace CodeBase.Gameplay.Mechanics.Root
{
    public class DragHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _dragableLayer;
        
        private Vector3 MousePosition => 
            new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        
        private DragableObject _dragableObject;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) StartDrag();

            if (Input.GetKey(KeyCode.Mouse0)) Drag();

            if (Input.GetKeyUp(KeyCode.Mouse0)) EndDrag();
        }

        private void StartDrag()
        {
            var collider = Physics2D.OverlapPoint(MousePosition, _dragableLayer);
            if (collider == null) return;

            if (collider.TryGetComponent(out DragableObject obj))
            {
                _dragableObject = obj;
                _dragableObject.transform.position = MousePosition;
                _dragableObject.StartDrag();
            }
        }

        private void Drag()
        {
            if (_dragableObject == null) return;
                
            _dragableObject.transform.position = MousePosition;
            _dragableObject.Drag();
        }

        private void EndDrag()
        {
            if (_dragableObject == null) return;
                
            _dragableObject.transform.position = MousePosition;
            _dragableObject.EndDrag();
            _dragableObject = null;
            
            if (YandexGame.GetFlag(Const.PROTECTION_FLAG_NAME) != Const.PROTECTION_FLAG_KEY)
                YandexGame.FullscreenShow();
        }
    }
}