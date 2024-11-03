using System;
using CodeBase.Utils;
using UnityEngine;
using YG;

namespace CodeBase.Gameplay.Mechanics
{
    public class DragHandler : MonoBehaviour
    {
        public static DragHandler Instance { get; private set; }
        public event Action OnDamagedClick;

        [SerializeField] private LayerMask[] _layerPriorityOrder;
        
        private Vector3 MousePosition => 
            new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, _zPos);
        
        private DragableObject _dragableObject;
        private float _zPos;
        private bool _isActive;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(this);
        }
        
        private void Update()
        {
            if (_isActive == false) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0)) OnStartHandle();

            if (Input.GetKey(KeyCode.Mouse0)) InProgressHandle();

            if (Input.GetKeyUp(KeyCode.Mouse0)) OnEndHandle();
        }

        public void Enable() => 
            _isActive = true;

        public void Disable()
        {
            _isActive = false;
            CancelDrag();
        }

        public void CancelDrag()
        {
            if (_dragableObject == null) return;
            
            _dragableObject.EndDrag();
            _dragableObject = null;
        }

        private void OnStartHandle()
        {
            foreach (LayerMask mask in _layerPriorityOrder)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero,  Mathf.Infinity, mask);
                
                if (hit)
                {
                    if (hit.transform.GetComponent<DamageTrigger>())
                    {
                        OnDamagedClick?.Invoke();
                        print("damaged");
                        return;
                    }
                
                    if (hit.transform.TryGetComponent(out DragableObject obj))
                    {
                        if (obj.IsActive == false)
                            return;
                        
                        _dragableObject = obj;
                        _zPos = _dragableObject.transform.position.z;
                        _dragableObject.transform.position = MousePosition;
                        _dragableObject.StartDrag();
                        return;
                    }

                    if (hit.transform.TryGetComponent(out IClickable clickable))
                    {
                        clickable.Click();
                        return;
                    }
                }
            }
        }

        private void InProgressHandle()
        {
            if (!_dragableObject) return;
            if (_dragableObject.IsActive == false) return;
            
            _dragableObject.transform.position = MousePosition;
            _dragableObject.Drag();
        }

        private void OnEndHandle()
        {
            if (_dragableObject == null) return;
                
            _dragableObject.transform.position = MousePosition;
            _dragableObject.EndDrag();
            _dragableObject = null;
            
            bool protectionEnabled = YandexGame.GetFlag(Const.ProtectionFlagName) != Const.ProtectionFlagKey;
            if (protectionEnabled == false)
                YandexGame.FullscreenShow();
        }
    }
}