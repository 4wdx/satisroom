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
        [SerializeField] private GameObject _nextLevelButton;
        [SerializeField] private GameObject _stars;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        private Canvas _canvas;
        
        private void Start()
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();
        }

        public void ShowResultWindow(int levelIndex)
        {
            _windowObject.gameObject.SetActive(true);
            _windowObject.DOMove(new Vector2(_canvas.pixelRect.width / 2, _canvas.pixelRect.height / 2), 0.4f);

            _stars.SetActive(SaveManager.IsCompleted(levelIndex));
            _nextLevelButton.SetActive(SaveManager.IsCompleted(levelIndex));
            
            _textMeshPro.text = SaveManager.IsCompleted(levelIndex) ? "Победа" : "Поражение";
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
    }
}