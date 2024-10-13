using UnityEngine;

namespace CodeBase.Root
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _parentSceneUI;
        [SerializeField] private Transform _rootParent;
        private AdNotificationYG _adNotificationYG;

        private void Start()
        {
            HideLoadingScreen();
            
            _adNotificationYG = FindFirstObjectByType<AdNotificationYG>();
            _adNotificationYG.notificationObj = _loadingScreen;
            _adNotificationYG.transform.parent = _rootParent;
        }

        public void ShowLoadingScreen() => 
            _loadingScreen.SetActive(true);

        public void HideLoadingScreen() => 
            _loadingScreen.SetActive(false);

        public void AttachSceneUI(GameObject sceneUI) => 
            sceneUI.transform.parent = _parentSceneUI;

        public void ClearChildUI()
        {
            for (var i = 0; i < _parentSceneUI.childCount; i++)
            {
                Destroy(_parentSceneUI.GetChild(i).gameObject);
            }
        }
    }
}