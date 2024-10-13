using CodeBase.Cfg;
using CodeBase.Gameplay.UI;
using CodeBase.Root;
using CodeBase.ServiceLocatorAPI;

using UnityEngine;

namespace CodeBase.Gameplay.Root
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private readonly GameplayExitInvoker _gameplayExitInvoker = new GameplayExitInvoker();
        private GameplayMediator _gameplayMediator;
        private GameplayUIMediator _uiMediator;
        private UIRoot _uiRoot;
        
        public GameplayExitInvoker Run(int levelIndex)
        {
            _gameplayExitInvoker.CurrentLevel = levelIndex;
            
            //create level
            var gameplayMediatorPrefab = ServiceLocator.Resolve<LevelQueue>().Levels[levelIndex];
            _gameplayMediator = Instantiate(gameplayMediatorPrefab);
            var uiMediatorPrefab = Resources.Load<GameplayUIMediator>("GameplayUI");
            _uiMediator = Instantiate(uiMediatorPrefab);
            
            //init level
            _gameplayMediator.Initialize(_gameplayExitInvoker, _uiMediator, levelIndex);
            ServiceLocator.Register(_gameplayMediator).AsSceneService();

            //attach ui
            _uiRoot = ServiceLocator.Resolve<UIRoot>();
            //_uiRoot.AttachSceneUI(_uiMediator.gameObject);
            
            //return invoker
            return _gameplayExitInvoker;
        }
    }
}