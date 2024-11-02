using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Root;
using CodeBase.Root;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CodeBase.Gameplay.UI
{
    public class GameplayUIMediator : MonoBehaviour
    {
        public event Action<ExitType> OnUISceneExit;
        public event Action OnForcedCloseLevel;
        public event Action OnShowHint;        
    
        [SerializeField] private RectTransform _windowObject;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Transform _hintParent;
        [SerializeField] private GameObject _hintCross;
        
        private Canvas _canvas;
        
        private void Start()
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();
        }

        public void ShowResultWindow(bool result)
        {
            _windowObject.gameObject.SetActive(true);
            _windowObject.DOMove(new Vector2(_canvas.pixelRect.width / 2, _canvas.pixelRect.height / 2), 0.4f);

            _winPanel.SetActive(result);
            _losePanel.SetActive(!result);
        }
        
        public void _ExitToMenu() => 
            OnUISceneExit?.Invoke(ExitType.ToMainMenu);

        public void _Restart() => 
            OnUISceneExit?.Invoke(ExitType.Restart);
        
        public void _OpenNextLevel() => 
            OnUISceneExit?.Invoke(ExitType.ToNext);

        public void _ForcedCloseLevel() => 
            OnForcedCloseLevel?.Invoke();

        public void _ShowHint() => 
            OnShowHint?.Invoke();

        public void OpenHint(GameObject hintPrefab)
        {
            GameObject hintObject = Instantiate(hintPrefab, _hintParent, false);
            _hintParent.gameObject.SetActive(true);
            _hintCross.gameObject.SetActive(true);
        }
    }
}