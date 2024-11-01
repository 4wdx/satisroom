using System;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class ClickTask : Task, IClickable
    {
        public event Action OnClicked;
        
        public void Click()
        {
            if (IsActive == false) return;
            
            StopTask();
        }
    }
}
