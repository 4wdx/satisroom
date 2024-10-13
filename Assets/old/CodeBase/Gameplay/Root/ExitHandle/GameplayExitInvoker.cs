using System;

namespace CodeBase.Gameplay.Root
{
    public class GameplayExitInvoker
    {
        public event Action<GameplayExitParams> OnGameplayExit;
        public int CurrentLevel;

        public void InvokeExit(ExitType exitType) => 
            OnGameplayExit?.Invoke(new GameplayExitParams(CurrentLevel, exitType));
    }

}