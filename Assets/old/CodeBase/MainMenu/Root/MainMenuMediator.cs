using UnityEngine;
using YG;

namespace CodeBase.MainMenu.Root
{
    public class MainMenuMediator : MonoBehaviour
    {
        private MainMenuExitInvoker _mainMenuExitInvoker;
        private ButtonHandler _buttonHandler;
        private TikTokFeed _tikTokFeed;
     
        
        public void Initialize(MainMenuExitInvoker mainMenuExitInvoker, TikTokFeed tikTokFeed, ButtonHandler buttonHandler)
        {
            _mainMenuExitInvoker = mainMenuExitInvoker;
            _buttonHandler = buttonHandler;
            _tikTokFeed = tikTokFeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                YandexGame.FullscreenShow();
        }

        public void StartGameplay(int targetLevel) => 
            _mainMenuExitInvoker.Invoke(targetLevel);

        public void BlockScroll() => 
            _tikTokFeed.Block();
        
        public void UnlockScroll() => 
            _tikTokFeed.Unlock();

        public void OnRewardedEnd()
        {
            _buttonHandler.ReconstructButtons();
            _tikTokFeed.ClearBlocklist();
        }
    }
}