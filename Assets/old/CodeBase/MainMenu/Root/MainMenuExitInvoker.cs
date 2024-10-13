using System;
using UnityEngine;

namespace CodeBase.MainMenu.Root
{
    public class MainMenuExitInvoker
    {
        public event Action<int> OnMenuExit;

        public void Invoke(int targetLevel) => 
            OnMenuExit?.Invoke(targetLevel);
    }
}