using CodeBase.Utils;
using UnityEngine;
using YG;

namespace CodeBase.MainMenu
{
    public class URLButton : MonoBehaviour
    {
        private void Start()
        {
            bool protectionEnabled = YandexGame.GetFlag(Const.ProtectionFlagName) != Const.ProtectionFlagKey;
            
            gameObject.SetActive(!protectionEnabled);
        }

        public void OpenTelegram()
        {
            Application.OpenURL(Const.TgURL);
        }
    }
}