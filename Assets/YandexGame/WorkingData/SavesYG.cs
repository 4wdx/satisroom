
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public bool[] OpenedLevels = new bool[14];
        public bool[] CompletedLevels = new bool[14];

        public bool sfxEnabled;
        public bool musicEnabled;

        
        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            OpenedLevels[0] = true;
            sfxEnabled = true;
            musicEnabled = true;
        }
    }
}
