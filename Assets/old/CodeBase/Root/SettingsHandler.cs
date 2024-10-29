using CodeBase.MainMenu;
using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace CodeBase.Root
{
    public class SettingsHandler : MonoBehaviour
    {
        [SerializeField] private ToggleButton _musicButton;
        [SerializeField] private ToggleButton _langButton;
        [SerializeField] private ToggleButton _sfxButton;

        [SerializeField] private AudioMixer _audioMixer;

        private void Start()
        {
            _musicButton.Init(YandexGame.savesData.musicEnabled);
            _sfxButton.Init(YandexGame.savesData.sfxEnabled);
            _langButton.Init(YandexGame.lang == "ru");
            print(YandexGame.lang);
            
            _musicButton.OnSwitchState += SetMusic;
            _langButton.OnSwitchState += SetLang;
            _sfxButton.OnSwitchState += SetSfx;

            _audioMixer.SetFloat("music", GetVolume(YandexGame.savesData.musicEnabled));
            _audioMixer.SetFloat("SFX", GetVolume(YandexGame.savesData.sfxEnabled));
        }

        private void SetMusic(bool value)
        {
            YandexGame.savesData.musicEnabled = value;
            _audioMixer.SetFloat("music", GetVolume(YandexGame.savesData.musicEnabled));
            YandexGame.SaveProgress();
        }

        private void SetSfx(bool value)
        {
            YandexGame.savesData.sfxEnabled = value;
            _audioMixer.SetFloat("SFX", GetVolume(YandexGame.savesData.sfxEnabled));
            YandexGame.SaveProgress();
        }

        private void SetLang(bool value)
        {
            print(GetLang(value));
            YandexGame.SwitchLanguage(GetLang(value)); 
            YandexGame.SaveProgress();
        }

        private float GetVolume(bool enabled) => enabled ? 0 : -80f;
        private string GetLang(bool enabled) => enabled ? "ru" : "en";
    }
}