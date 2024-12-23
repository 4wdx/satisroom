using CodeBase.ServiceLocatorAPI;
using CodeBase.Root;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.MainMenu.Root
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuMediator _mainMenuMediator;
        [FormerlySerializedAs("_buttonHandler")] [SerializeField] private PageHandler _pageHandler;
        [SerializeField] private TikTokFeed _tikTokFeed;
        [SerializeField] private GameObject _ui;
        
        private MainMenuExitInvoker _sceneExitInvoker = new MainMenuExitInvoker();
        private UIRoot _uiRoot;

        public MainMenuExitInvoker Run()
        {
            _mainMenuMediator.Initialize(_sceneExitInvoker, _tikTokFeed, _pageHandler);
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