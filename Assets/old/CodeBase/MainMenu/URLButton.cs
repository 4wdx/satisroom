using CodeBase.Utils;
using UnityEngine;
using YG;

namespace CodeBase.MainMenu
{
    public class URLButton : MonoBehaviour
    {
        private void Start()
        {
            if (YandexGame.GetFlag(Const.PROTECTION_FLAG_NAME) != Const.PROTECTION_FLAG_KEY)
                gameObject.SetActive(false);
        }

        public void OpenTelegram()
        {
            Application.OpenURL(Const.TG_URL);
        }
    }
}