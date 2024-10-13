using CodeBase.MainMenu.Root;
using CodeBase.ServiceLocatorAPI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.MainMenu
{
    public class SwipeBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private MainMenuMediator _mainMenuMediator;

        private void Start() => 
            _mainMenuMediator = ServiceLocator.Resolve<MainMenuMediator>();

        public void OnPointerEnter(PointerEventData eventData)
            => _mainMenuMediator.BlockScroll();

        public void OnPointerExit(PointerEventData eventData)
            => _mainMenuMediator.UnlockScroll();
    }
}