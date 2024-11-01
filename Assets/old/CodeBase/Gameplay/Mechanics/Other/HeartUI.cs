using System;
using CodeBase.Gameplay.Levels;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hearts;

        private HeartLevel _heartLevel;
        
        private void Start()
        {
            _heartLevel = FindFirstObjectByType<HeartLevel>();
            
            if (_heartLevel == null)
                DisableUI();
            else 
                _heartLevel.OnHealthChanged += UpdateUI;
        }
        
        private void UpdateUI(int currentHealth)
        {
            if (currentHealth <= 0)
                DisableUI();
            
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < currentHealth)
                    _hearts[i].SetActive(true);
                else 
                    _hearts[i].SetActive(false);
            }
        }

        private void DisableUI() => 
            gameObject.SetActive(false);

        private void OnDestroy()
        {
            if (_heartLevel != null)
                _heartLevel.OnHealthChanged -= UpdateUI;
        }
    }
}
