using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform _follow;

        private void LateUpdate()
        {
            transform.position = _follow.position;
        }
    }
}
