using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        public event Action<bool> OnSwitchState; 
        
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        private Button _button;
        private bool _active;        
        
        public void Init(bool active)
        {
            _active = active;
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SwitchState);

            print(_active);
            _button.image.sprite = _active ? _enabledSprite : _disabledSprite;
        }
        
        private void SwitchState()
        {
            _active = !_active;
            _button.image.sprite = _active ? _enabledSprite : _disabledSprite;
            
            print(_active);
            OnSwitchState?.Invoke(_active);
        }
    }
}
