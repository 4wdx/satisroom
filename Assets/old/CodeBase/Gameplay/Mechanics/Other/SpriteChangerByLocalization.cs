using UnityEngine;
using YG;

namespace CodeBase.Gameplay.Mechanics
{
    public class SpriteChangerByLocalization : MonoBehaviour
    {
        [SerializeField] private GameObject _ru;
        [SerializeField] private GameObject _en;

        private void OnEnable()
        {
            _ru.SetActive(YandexGame.lang != "en");
            _en.SetActive(YandexGame.lang == "en");
        }
    }
}