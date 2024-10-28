using CodeBase.Utils;
using UnityEngine;
using YG;

namespace CodeBase.MainMenu
{
    public class URLButton : MonoBehaviour
    {
        private void Start()
        {
            if (YandexGame.GetFlag(Const.ProtectionFlagName) != Const.ProtectionFlagKey)
                gameObject.SetActive(false);
        }

        public void OpenTelegram()
        {
            Application.OpenURL(Const.TgURL);
        }
    }
}