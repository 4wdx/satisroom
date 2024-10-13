using System;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public abstract class Level : MonoBehaviour
    {
        public abstract event Action<bool> OnCompleted;
    }
}