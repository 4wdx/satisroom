using UnityEngine;

namespace CodeBase.Cfg
{
    [CreateAssetMenu(menuName = "Satisroom/Hint stack")]
    public class HintStack : ScriptableObject
    {
        [field: SerializeField] public GameObject[] _hints;
    }
}
