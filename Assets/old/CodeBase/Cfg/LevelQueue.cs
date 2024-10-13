using CodeBase.Gameplay.Root;
using UnityEngine;

namespace CodeBase.Cfg
{
    [CreateAssetMenu(menuName = "Satisroom/Level Queue")]
    public class LevelQueue : ScriptableObject
    {
        [field: SerializeField] public GameplayMediator[] Levels { get; private set; }
    }
}