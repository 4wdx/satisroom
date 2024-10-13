using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

namespace CodeBase.MainMenu
{
    public class TikTokFeed : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float _distanceToSwipe;
        [SerializeField] private float _cancelSpeed;
        [SerializeField] private float _swipeSpeed;
        [SerializeField] private Canvas _rootCanvas;
        [SerializeField] private ButtonHandler _buttonHandler;

        [SerializeField] private RectTransform _topPage;
        [SerializeField] private RectTransform _botPage;
        
        private float _height;
        private bool _dragging;
        private bool _automoving;
        private RectTransform _rectTransform;
        private Vector2 _startDragPosition;

        [SerializeField] private int _blockCount;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _height = _rootCanvas.GetComponent<RectTransform>().rect.height;
            
            _topPage.offsetMax = new Vector2(_rectTransform.offsetMax.x, _height);
            _topPage.offsetMin = new Vector2(_rectTransform.offsetMax.x, _height);
            
            _botPage.offsetMax = new Vector2(_rectTransform.offsetMax.x, -_height);
            _botPage.offsetMin = new Vector2(_rectTransform.offsetMax.x, -_height);
        }

        public void ClearBlocklist() =>
            _blockCount = 0;

        public void Block() => 
            _blockCount++;

        public void Unlock() => 
            _blockCount--;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_automoving) return;
            if (_blockCount != 0) return;

            _dragging = true;
            _startDragPosition = _rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_automoving) return;
            if (_blockCount != 0) return;

            if (_dragging)
            {
                var delta = new Vector2(0, eventData.delta.y);
                _rectTransform.anchoredPosition += delta / _rootCanvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_automoving) return;

            if (_dragging)
            {
                //if feed moving more than need for swipe
                if (Vector2.Distance(_startDragPosition, _rectTransform.anchoredPosition) > _distanceToSwipe) 
                    StartCoroutine(Swipe(Mathf.Sign(_rectTransform.anchoredPosition.y - _startDragPosition.y)));
                else
                    StartCoroutine(CancelSwipe());
            
                _automoving = true;
                _dragging = false;
            }
        }

        private IEnumerator Swipe(float sign)
        {
            Vector2 targetPos;
            bool direction; //true -- up, false -- down
            if (sign > 0)
            {
                targetPos = new Vector2(_startDragPosition.x, _startDragPosition.y + _height);
                direction = true;
            }
            else
            {
                targetPos = new Vector2(_startDragPosition.x, _startDragPosition.y - _height);
                direction = false;
            }
            
            while (Vector2.Distance(targetPos, _rectTransform.anchoredPosition) > 0.1f)
            {
                _rectTransform.anchoredPosition = Vector2.MoveTowards(
                    _rectTransform.anchoredPosition, 
                    targetPos, 
                    _swipeSpeed * Time.deltaTime);
                
                print(_rectTransform.anchoredPosition.y + "/" + targetPos.y + "Height:" + _height);
                yield return null;
            }

            _rectTransform.anchoredPosition = targetPos;
            yield return null; 
            _rectTransform.anchoredPosition = _startDragPosition;
            
            if (direction)
                _buttonHandler.NextPage();
            else 
                _buttonHandler.PreviousPage();
            
            _automoving = false;
        }

        private IEnumerator CancelSwipe()
        {
            while (Vector2.Distance(_startDragPosition, _rectTransform.anchoredPosition) > 0.1f)
            {
                _rectTransform.anchoredPosition = Vector2.MoveTowards(
                    _rectTransform.anchoredPosition, 
                    _startDragPosition, 
                    _cancelSpeed * Time.deltaTime);
                
                yield return null;
            }

            _rectTransform.anchoredPosition = _startDragPosition;
            _startDragPosition = Vector2.zero;
            _automoving = false;
        }
    }
}