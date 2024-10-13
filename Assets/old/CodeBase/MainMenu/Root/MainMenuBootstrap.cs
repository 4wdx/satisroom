using CodeBase.ServiceLocatorAPI;
using CodeBase.Root;
using UnityEngine;

namespace CodeBase.MainMenu.Root
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuMediator _mainMenuMediator;
        [SerializeField] private ButtonHandler _buttonHandler;
        [SerializeField] private TikTokFeed _tikTokFeed;
        [SerializeField] private GameObject _ui;
        
        private MainMenuExitInvoker _sceneExitInvoker = new MainMenuExitInvoker();
        private UIRoot _uiRoot;

        public MainMenuExitInvoker Run()
        {
            _mainMenuMediator.Initialize(_sceneExitInvoker, _tikTokFeed, _buttonHandler);
            ServiceLocator.Register(_mainMenuMediator).AsSceneService();
            
            //attach scene ui
            _uiRoot = ServiceLocator.Resolve<UIRoot>();
            _uiRoot.AttachSceneUI(_ui);
            
            //return invoker
            //_sceneExitInvoker = new MainMenuExitInvoker();
            return _sceneExitInvoker;
        }
    }
}