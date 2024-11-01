using UnityEngine;
using YG;

namespace CodeBase.Utils
{
    public class StartGameMetricaSend : MonoBehaviour
    {
        private static bool _sended;
        
        private void Start()
        {
            if (_sended == true) return;
            
            YandexMetrica.Send(Const.StartGameMetricaName);
            _sended = true;
        }
    }
}
