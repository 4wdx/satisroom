using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace CodeBase.Root
{
    public class SettingsHandler : MonoBehaviour
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _langToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        [SerializeField] private AudioMixer _audioMixer;

        private void Start()
        {
            _musicToggle.isOn = YandexGame.savesData.musicEnabled;
            _sfxToggle.isOn = YandexGame.savesData.sfxEnabled;
            _langToggle.isOn = YandexGame.lang == "ru";
            
            _musicToggle.onValueChanged.AddListener(SetMusic);
            _langToggle.onValueChanged.AddListener(SetLang);
            _sfxToggle.onValueChanged.AddListener(SetSfx);

            _audioMixer.SetFloat("music", GetVolume(YandexGame.savesData.musicEnabled));
            _audioMixer.SetFloat("SFX", GetVolume(YandexGame.savesData.sfxEnabled));
            _textMeshPro.text = "lang: " + YandexGame.lang;
        }

        private void SetMusic(bool value)
        {
            YandexGame.savesData.musicEnabled = value;
            _audioMixer.SetFloat("music", GetVolume(YandexGame.savesData.musicEnabled));
            YandexGame.SaveProgress();
            print("Music: " + value);
        }

        private void SetSfx(bool value)
        {
            YandexGame.savesData.sfxEnabled = value;
            _audioMixer.SetFloat("music", GetVolume(YandexGame.savesData.sfxEnabled));
            YandexGame.SaveProgress();
            print("SFX: " + value);
        }

        private void SetLang(bool value)
        {
            YandexGame.lang = GetLang(value);
            YandexGame.SaveProgress();

            _textMeshPro.text = "lang: " + GetLang(value);
            print("Lang: " + GetLang(value));
        }

        private float GetVolume(bool enabled) => enabled ? 0 : -80f;
        private string GetLang(bool enabled) => enabled ? "ru" : "en";
    }
}