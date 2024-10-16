using CodeBase.Gameplay.Mechanics.Root;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class ClickTask : Task, IClickable
    {
        public void Click()
        {
            if (IsActive == false) return;
            
            StopTask();
        }
    }
}
