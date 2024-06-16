using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private Sprite _OffSound, _OnSound;
    [SerializeField] private GameObject _windowSettings, _windowLelelHard;
    [SerializeField] private Toggle _musicToggle, _soundToggle, _darkThemeToggle;
    [SerializeField] private AudioMixer _sound;
    private bool _isCoop;
    private const int _level = 1;
    private float _soundVolum;
    private Theme _theme;

    private void Awake()
    {
        Time.timeScale = 1;
        var oneMusic = FindObjectsOfType<AudioSource>();
        _music = oneMusic[0];
        _soundVolum = PlayerPrefs.GetInt("Music", 1);
        if (oneMusic.Length > 1)
        {
            Destroy(oneMusic[oneMusic.Length - 1].gameObject);
        }
        DontDestroyOnLoad(_music);
    }

    public void ChoiseLevelHard(int countCell)
    {
        CreateGrid.CountActiveCell = countCell;
        Play();
    }

    public void Coop()
    {
        _isCoop = true;
        _windowLelelHard.gameObject.SetActive(true);
    }

    public void ActiveWindowChoiseLevelHard()
    {
        _windowLelelHard.SetActive(true);
    }
    private void Start()
    {
        _theme = Theme.Global;
        _darkThemeToggle.isOn = PlayerPrefs.GetInt("Theme", 0) == 0 ? false : true;
        _musicToggle.isOn = _soundVolum == 1 ? true : false;
        _soundToggle.isOn = PlayerPrefs.GetInt("Sound", 0) == 0 ? true : false;
        Sound();
    }
    public void Sound()
    {
        _music.volume = _soundVolum;
    }

    public void Play()
    {
        if (!_isCoop)
        {
            SceneManager.LoadScene(_level);
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        _windowSettings.SetActive(true);
    }

    public void ToggleSound()
    {
        int volumeSound = _soundToggle.isOn ? 0 : -80;
        PlayerPrefs.SetInt("Sound", volumeSound);
        _sound.SetFloat("MyExposedParam", volumeSound);
    }
    public void ToggleMusic()
    {
        int volumeMusic = _musicToggle.isOn ? 1 : 0;
        print(volumeMusic);
        PlayerPrefs.SetInt("Music", volumeMusic);
        _music.volume = volumeMusic;

    }

    public void ThemeChange()
    {
        print(_darkThemeToggle.isOn);
        PlayerPrefs.SetInt("Theme", _darkThemeToggle.isOn ? 1 : 0);
        _theme.SetTheme(_darkThemeToggle.isOn);
    }
    public void Back()
    {
        _windowSettings.SetActive(false);
    }
}
