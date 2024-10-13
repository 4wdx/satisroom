namespace CodeBase.Gameplay.Root
{
    public struct GameplayExitParams
    {
        public readonly int LevelIndex;
        public readonly ExitType ExitType;
        
        public GameplayExitParams(int levelIndex, ExitType exitType)
        {
            LevelIndex = levelIndex;
            ExitType = exitType;
        }
    }
}
