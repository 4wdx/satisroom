using CodeBase.Cfg;
using CodeBase.Root;
using CodeBase.ServiceLocatorAPI;
using UnityEngine;

namespace CodeBase.MainMenu
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private LoadGameplayButton[] _buttons;

        private LevelQueue _levelQueue;
        private int _currentUpIndex;

        private void Start()
        {
            _levelQueue = ServiceLocator.Resolve<LevelQueue>();
            print(SaveManager.LevelCount);
            _currentUpIndex = 12;
            
            ReconstructButtons();
        }

        public void NextPage()
        {
            _currentUpIndex += 2;
            
            if (_currentUpIndex > SaveManager.LevelCount)
            {
                var newValue = _currentUpIndex - SaveManager.LevelCount;

                _currentUpIndex = newValue;
            }
            
            ReconstructButtons();
        }

        public void PreviousPage()
        {
            _currentUpIndex -= 2;
            
            if (_currentUpIndex < 0)
            {
                var newValue = SaveManager.LevelCount + _currentUpIndex;

                _currentUpIndex = newValue;
            }
            
            ReconstructButtons();
        }
        
        public void ReconstructButtons()
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var index = (_currentUpIndex + i) % SaveManager.LevelCount;
                _buttons[i].ReConstruct(index, _levelQueue.Levels[index].LevelIcon);
            }
        }
    }
}