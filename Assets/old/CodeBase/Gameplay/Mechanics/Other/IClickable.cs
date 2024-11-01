using System;

namespace CodeBase.Gameplay.Mechanics
{
    public interface IClickable
    {
        event Action OnClicked;
        
        public void Click();
    }
}
