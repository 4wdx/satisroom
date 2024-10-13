using System;
using YG;

namespace CodeBase.Root
{
    public static class SaveManager
    {
        public static int LevelCount => YandexGame.savesData.CompletedLevels.Length;
        
        public static void LevelComplete(int levelIndex)
        {
            if (levelIndex is < 0 or > 13)
                throw new Exception("Invalid level index");
            
            YandexGame.savesData.CompletedLevels[levelIndex] = true;
            
            if (YandexGame.savesData.OpenedLevels[levelIndex + 1] == false) //if next closed, open
                YandexGame.savesData.OpenedLevels[levelIndex + 1] = true;
            
            YandexGame.SaveProgress();
        }

        public static void OpenLevel(int levelIndex)
        {
            if (levelIndex is < 0 or > 13)
                throw new Exception("Invalid level index");
            
            YandexGame.savesData.OpenedLevels[levelIndex] = true;
            YandexGame.SaveProgress();
        }

        public static bool IsOpened(int levelIndex) => 
            YandexGame.savesData.OpenedLevels[levelIndex];
        
        public static bool IsCompleted(int levelIndex) => 
            YandexGame.savesData.CompletedLevels[levelIndex];
    }
}