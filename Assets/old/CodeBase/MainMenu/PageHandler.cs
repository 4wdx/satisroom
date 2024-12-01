using CodeBase.Root;
using UnityEngine;

namespace CodeBase.MainMenu
{
    public class PageHandler : MonoBehaviour
    {
        [SerializeField] private LevelPage[] _upPages;
        [SerializeField] private LevelPage[] _mainPages;
        [SerializeField] private LevelPage[] _bottomPages;
        
        [SerializeField] private int _currentUpIndex;

        private void Start()
        {
            _currentUpIndex += 0;

            for (int i = 0; i < _upPages.Length; i++)
            {
                _upPages[i].Initialize(i);
                _upPages[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < _mainPages.Length; i++)
            {
                _mainPages[i].Initialize(i);
                _mainPages[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < _bottomPages.Length; i++)
            {
                _bottomPages[i].Initialize(i);
                _bottomPages[i].gameObject.SetActive(false);
            }
            
            ReconstructPages();
        }

        public void NextPage()
        {
            DisableCurrentPages();
            _currentUpIndex++;
            ReconstructPages();
        }

        public void PreviousPage()
        {
            DisableCurrentPages();
            _currentUpIndex--;
            ReconstructPages();
        }
        
        public void ReconstructPages()
        {
            int index = _currentUpIndex % SaveManager.LevelCount;
            int topIndex = index - 1;
            int bottomIndex = index + 1;
            
            if (topIndex < 0) 
                topIndex = SaveManager.LevelCount - 1;
            if (bottomIndex >= SaveManager.LevelCount)
                bottomIndex = 0;
            
            _upPages[topIndex].gameObject.SetActive(true);

            
            _mainPages[index].gameObject.SetActive(true);

            
            _bottomPages[bottomIndex].gameObject.SetActive(true);
        }

        private void DisableCurrentPages()
        {
            int index = _currentUpIndex % SaveManager.LevelCount;
            int topIndex = index - 1;
            int bottomIndex = index + 1;
            
            if (topIndex < 0) 
                topIndex = SaveManager.LevelCount - 1;
            if (bottomIndex >= SaveManager.LevelCount)
                bottomIndex = 0;
            
            _upPages[topIndex].gameObject.SetActive(false);

            
            _mainPages[index].gameObject.SetActive(false);

            
            _bottomPages[bottomIndex].gameObject.SetActive(false);
        }
    }
}