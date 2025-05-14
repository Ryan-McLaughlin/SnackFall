using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsManager: MonoBehaviour
{
    public static OptionsManager Instance { get; private set; }

    public AudioMixer gameAudioMixer; // Assign your Audio Mixer in the Inspector
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    public bool isGamePaused { get; private set; } = false;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadAudioSettings()
    {
        if(gameAudioMixer != null)
        {
            SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 0f)); // Default to max volume (0dB)
            SetSFXVolume(PlayerPrefs.GetFloat(SFXVolumeKey, 0f));   // Default to max volume (0dB)
        }
    }

    public void ToggleMusicMute()
    {
        float currentVolume;
        gameAudioMixer.GetFloat("MusicVolume", out currentVolume);
        if(currentVolume <= -80f) // Effectively muted
        {
            SetMusicVolume(0f);
        }
        else
        {
            SetMusicVolume(-80f);
        }
    }

    public void ToggleSFXMute()
    {
        float currentVolume;
        gameAudioMixer.GetFloat("SFXVolume", out currentVolume);
        if(currentVolume <= -80f) // Effectively muted
        {
            SetSFXVolume(0f);
        }
        else
        {
            SetSFXVolume(-80f);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if(gameAudioMixer != null)
        {
            gameAudioMixer.SetFloat("MusicVolume", volume);
            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if(gameAudioMixer != null)
        {
            gameAudioMixer.SetFloat("SFXVolume", volume);
            PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene("GameScene"); // Assuming your game scene is named "GameScene"
    }

    public void ExitToTitle()
    {
        ResumeGame();
        SceneManager.LoadScene("TitleScene"); // Assuming your title scene is named "TitleScene"
    }
}